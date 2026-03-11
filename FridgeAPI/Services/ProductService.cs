using Fridge.API.Repositories;
using Fridge.Core.Models;

namespace Fridge.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product ? GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }
        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
        }
        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }
        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
        }
        public bool ProductExists(int id)
        {
            return _productRepository.Exists(id);
        }
    }
}
