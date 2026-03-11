using Fridge.API.Repositories;
using Fridge.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fridge.API.Services
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
        public List<FridgeProduct>  GetProductInFridge(int fridgeid)
        {
            return _fridgeProductRepository.GetByFridgeId(fridgeid);
        }
        public void AddProductToFridge(int  fridgeid, int productid)
        {
            if(!_fridgeRepository.Exists(fridgeid))
            {
                throw new Exception("fridge not found");
            }
            if(!_productRepository.Exists(productid))
            {
                throw new Exception("product not found");
            }
            var existingFridgeProduct = _fridgeProductRepository.Get(fridgeid, productid);

            if(existingFridgeProduct !=null)
            {
                existingFridgeProduct.Quantity += 1;
                _fridgeProductRepository.Update(existingFridgeProduct);
            }
            else
            {
                var newFridgeProduct = new FridgeProduct
                {
                    FridgeId = fridgeid,
                    ProductId = productid,
                    Quantity = 1
                };
                _fridgeProductRepository.Add(newFridgeProduct);
            }
        }
        public void RemoveProductFromFridge(int fridgeid, int productid)
        {
            if (!_fridgeProductRepository.Exists(fridgeid, productid))
                {
                throw new Exception("product in fridge not found");
                 }
            _fridgeProductRepository.Delete(fridgeid, productid);
            _fridgeProductRepository.Save();
        }
        public void UpdateProductQuantity(int fridgeid, int quantity,int productid)
        {
            var fridgeProduct = _fridgeProductRepository.Get(fridgeid,productid);
            if(fridgeProduct==null)
            {
                throw new Exception("product in fridge not found");
            }
            fridgeProduct.Quantity = quantity;
            _fridgeProductRepository.Update(fridgeProduct);
            _fridgeProductRepository.Save();
            if(quantity==0)
            {
                _refillRepository.Refill();
            }
        }
    }
}
