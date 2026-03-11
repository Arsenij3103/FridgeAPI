using Fridge.Core.Models;

namespace Fridge.API.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        bool ProductExists(int id);
    }
}
