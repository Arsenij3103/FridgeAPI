using Fridge.Core.Models;
namespace Fridge.API.Repositories

{
    public interface IFridgeRepository
    {
        List<FridgeEntity> GetAll();
        FridgeEntity? GetById(int id);
        void Add(FridgeEntity fridge);
        bool Exists(int id);
        void Save();
    }
}
