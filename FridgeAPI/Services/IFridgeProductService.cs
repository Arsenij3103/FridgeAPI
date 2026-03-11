using Fridge.Core.Models;

namespace Fridge.API.Services
{
    public interface IFridgeProductService
    {
        List<FridgeProduct> GetProductInFridge(int fridgeid);
        void AddProductToFridge(int fridgeid,int  productid);
        void RemoveProductFromFridge(int fridgeid, int productid);
        void UpdateProductQuantity(int fridgeid,int quantity, int productid);
        
    }
}
