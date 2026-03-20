using Fridge.Domain.Entities;
namespace Fridge.Aplication.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product>? GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task SaveAsync();
        Task DeleteAsync(int id);
    }
}
