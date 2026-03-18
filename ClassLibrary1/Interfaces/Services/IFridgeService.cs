using Fridge.Domain.Entities;

namespace Fridge.Aplication.Interfaces.Services
{
    public interface IFridgeService
    {
        Task<List<FridgeResponce>> GetAllFridgesAsync();
        Task<FridgeResponce>? GetFridgeByIdAsync(int id);
        Task CreateFridgeAsync(string name);
        Task<bool> FridgeExistsAsync(int id);
    }
}
