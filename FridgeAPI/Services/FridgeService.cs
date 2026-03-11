using Fridge.API.Repositories;
using Fridge.Core.Models;

namespace Fridge.API.Services
{
    public class FridgeService: IFridgeService
    {
        private readonly IFridgeRepository _fridgeRepository;
        public FridgeService(IFridgeRepository fridgeRepository)
        {
            _fridgeRepository=fridgeRepository;
        }

        public List<FridgeEntity> GetAllFridges()
        {
            return _fridgeRepository.GetAll();
        }
        public FridgeEntity ? GetFridgeById(int id)
        {
            return _fridgeRepository.GetById(id);
        }
        public void CreateFridge(string name)
        {
            var fridge = new FridgeEntity
            {
                Name = name
            };
            _fridgeRepository.Add(fridge);
            _fridgeRepository.Save();
        }
        public bool FridgeExists(int id)
        {
            return _fridgeRepository.Exists(id);
        }
    }
}
