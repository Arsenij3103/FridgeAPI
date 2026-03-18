using Fridge.Domain.Entities;
namespace Fridge.Aplication.Interfaces.Repositories
{

    public interface IFridgeRepository
    {
        Task<List<FridgeResponce>> GetAllAsync();
        Task<FridgeResponce>? GetByIdAsync(int id);
        Task AddAsync(FridgeResponce fridge);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
