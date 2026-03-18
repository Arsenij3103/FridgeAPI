using Fridge.Domain.Entities;
namespace Fridge.API.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int id);
        bool Exists(int id);
        void Add(Product product);
        void Update(Product product);
        void Save();
        void Delete(int id);
    }
}
