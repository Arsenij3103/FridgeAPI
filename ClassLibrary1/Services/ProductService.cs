
using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Aplication.Interfaces.Services;
using Fridge.Application.Exceptions;
using Fridge.Domain.Entities;

namespace Fridge.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product>? GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task CreateProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new BadRequestException("Product name is required");
            }
            if (product.DefaultQuantity < 0)
            {
                throw new BadRequestException("Default quantity cannot be negative");
            }
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with id {product.Id} was not found");
            }
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new BadRequestException("Product name is requried");
            }
            if (product.DefaultQuantity < 0)
            {
                throw new BadRequestException("Default quantity cannot be negative");
            }
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with id{id} was not found");
            }

            await _productRepository.DeleteAsync(id);
            await _productRepository.SaveAsync();
        }
        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _productRepository.ExistsAsync(id);
        }
    }
}
