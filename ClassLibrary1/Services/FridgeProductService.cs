using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Aplication.Interfaces.Services;
using Fridge.Application.Exceptions;
using Fridge.Domain.Entities;


namespace Fridge.Aplication.Services
{
    public class FridgeProductService : IFridgeProductService
    {
        private readonly IFridgeProductRepository _fridgeProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFridgeRepository _fridgeRepository;
        private readonly IRefillRepository _refillRepository;
        public FridgeProductService(IFridgeProductRepository fridgeProductRepository, IProductRepository productRepository,
            IFridgeRepository fridgeRepository, IRefillRepository refillRepository)
        {
            _fridgeProductRepository = fridgeProductRepository;
            _productRepository = productRepository;
            _refillRepository = refillRepository;
            _fridgeRepository = fridgeRepository;
        }
        public async Task<List<FridgeProduct>> GetProductsByFridgeIdAsync(int fridgeid)
        {
            var fridgeExists = await _fridgeRepository.ExistsAsync(fridgeid);
            if (!fridgeExists)
            {
                throw new NotFoundException($"Fridge with {fridgeid} was not found");
            }
            return await _fridgeProductRepository.GetByFridgeIdAsync(fridgeid);
        }
        public async Task? AddProductToFridgeAsync(int fridgeid, int productid)
        {
            var fridge = await _fridgeRepository.GetByIdAsync(fridgeid);

            if (fridge == null)
            {
                throw new NotFoundException($"Fridge with {fridgeid} was not found");
            }
            var product = await _productRepository.GetByIdAsync(productid);
            if (product == null)
            {
                throw new NotFoundException($"Product with {productid} was not found");
            }
            var existingFridgeProduct = await _fridgeProductRepository.GetAsync(fridgeid, productid);

            if (existingFridgeProduct != null)
            {
                existingFridgeProduct.Quantity += product.DefaultQuantity;
                await _fridgeProductRepository.UpdateAsync(existingFridgeProduct);
            }
            else
            {
                var fridgeProduct = new FridgeProduct
                {
                    FridgeId = fridgeid,
                    ProductId = productid,
                    Quantity = product.DefaultQuantity,
                };
                await _fridgeProductRepository.AddAsync(fridgeProduct);
            }
            await _fridgeProductRepository.SaveAsync();
        }
        public async Task RemoveProductFromFridgeAsync(int fridgeid, int productid)
        {
            var fridgeProduct = await _fridgeProductRepository.GetAsync(fridgeid, productid);
            if (fridgeProduct == null)
            {
                throw new NotFoundException($"Product whith id {productid} in fridge with id{fridgeid} was not found");
            }
            await _fridgeProductRepository.DeleteAsync(fridgeid, productid);
            await _fridgeProductRepository.SaveAsync();
        }
        public async Task UpdateProductQuantityAsync(int fridgeId, int quantity, int productId)
        {
            if (quantity < 0)
            {
                throw new BadRequestException("Quantity cannot be negative");
            }
            var fridgeProduct = await _fridgeProductRepository.GetAsync(fridgeId, productId);
            if (fridgeProduct == null)
            {
                throw new NotFoundException($"Product whith id {productId} in fridge with id{fridgeId} was not found");
            }
            fridgeProduct.Quantity = quantity;
            await _fridgeProductRepository.UpdateAsync(fridgeProduct);
            await _fridgeProductRepository.SaveAsync();
            if (quantity == 0)
            {
                await _refillRepository.RefillAsync(fridgeId, productId);
            }
        }

    }
}
