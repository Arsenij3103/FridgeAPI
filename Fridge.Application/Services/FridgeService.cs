using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Aplication.Interfaces.Services;
using Fridge.Application.Exceptions;
using Fridge.Domain.Entities;

namespace Fridge.Aplication.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly IFridgeRepository _fridgeRepository;
        public FridgeService(IFridgeRepository fridgeRepository)
        {
            _fridgeRepository = fridgeRepository;
        }

        public async Task<List<FridgeResponce>> GetAllFridgesAsync()
        {
            return await _fridgeRepository.GetAllAsync();
        }
        public async Task<FridgeResponce>? GetFridgeByIdAsync(int id)
        {
            return await _fridgeRepository.GetByIdAsync(id);
        }
        public async Task CreateFridgeAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new BadRequestException("Fridge name is required");
            }
            var fridge = new FridgeResponce
            {
                Name = name
            };
            await _fridgeRepository.AddAsync(fridge);
            await _fridgeRepository.SaveAsync();
        }
        public async Task<bool> FridgeExistsAsync(int id)
        {
            return await _fridgeRepository.ExistsAsync(id);
        }
    }
}
