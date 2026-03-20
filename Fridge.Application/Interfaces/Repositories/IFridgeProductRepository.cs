using Fridge.Domain.Entities;


namespace Fridge.Aplication.Interfaces.Repositories;

public interface IFridgeProductRepository
{
    Task<List<FridgeProduct>> GetByFridgeIdAsync(int fridgeid);

    Task<FridgeProduct>? GetAsync(int fridgeid, int productid);
    Task<bool> ExistsAsync(int fridgeid, int productid);
    Task AddAsync(FridgeProduct fridgeProduct);
    Task UpdateAsync(FridgeProduct fridgeProduct);
    Task DeleteAsync(int fridgeid, int productid);
    Task SaveAsync();
}
