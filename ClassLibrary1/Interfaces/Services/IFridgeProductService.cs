using Fridge.Domain.Entities;

namespace Fridge.Aplication.Interfaces.Services;

public interface IFridgeProductService
{
    Task<List<FridgeProduct>> GetProductsByFridgeIdAsync(int fridgeid);
    Task AddProductToFridgeAsync(int fridgeid, int productid);
    Task RemoveProductFromFridgeAsync(int fridgeid, int productid);
    Task UpdateProductQuantityAsync(int fridgeid, int quantity, int productid);

}
