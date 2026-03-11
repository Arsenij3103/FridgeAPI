using Fridge.Core.Models;

namespace Fridge.API.Services
{
    public interface IFridgeService
    {
        List<FridgeEntity> GetAllFridges();
        FridgeEntity? GetFridgeById(int id);
        void CreateFridge(string name);
        bool FridgeExists(int id);
    }
}
