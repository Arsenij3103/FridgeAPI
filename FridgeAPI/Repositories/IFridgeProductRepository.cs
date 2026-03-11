using Fridge.Core.Models;
using Fridge.API.Data;

namespace Fridge.API.Repositories
{
    public interface IFridgeProductRepository
    {
        List<FridgeProduct> GetByFridgeId(int fridgeid);

        FridgeProduct ? Get(int fridgeid, int productid);
        bool Exists(int fridgeid, int productid);
        void Add(FridgeProduct fridgeProduct);
        void Update(FridgeProduct fridgeProduct);
        void Delete(int fridgeid, int productid);
        void Save();
    }
}
