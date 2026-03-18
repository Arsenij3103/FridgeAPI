using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Domain.Entities;
using Fridge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure.Repositories
{
    public class FridgeRepository : IFridgeRepository
    {
        private readonly AppDbContext _context;

        public FridgeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FridgeResponce>> GetAllAsync()
        {
            return await _context.Fridges.ToListAsync();
        }

        public async Task<FridgeResponce>? GetByIdAsync(int id)
        {
            return await _context.Fridges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(FridgeResponce fridge)
        {
            await _context.Fridges.AddAsync(fridge);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Fridges.AnyAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
