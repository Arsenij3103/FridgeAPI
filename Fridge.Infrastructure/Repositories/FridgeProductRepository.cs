
using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Domain.Entities;
using Fridge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Fridge.Infrastructure.Repositories
{
    public class FridgeProductRepository : IFridgeProductRepository
    {
        private readonly AppDbContext _context;

        public FridgeProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FridgeProduct>> GetByFridgeIdAsync(int fridgeid)
        {
            return await _context.FridgeProducts
               .Where(x => x.FridgeId == fridgeid)
               .ToListAsync();
        }
        public async Task<FridgeProduct>? GetAsync(int fridgeid, int productid)
        {
            return await _context.FridgeProducts
                .FirstOrDefaultAsync(x => x.FridgeId == fridgeid && x.ProductId == productid);
        }

        public async Task<bool> ExistsAsync(int fridgeid, int productid)
        {
            return await _context.FridgeProducts
                .AnyAsync(x => x.FridgeId == fridgeid && x.ProductId == productid);
        }

        public Task UpdateAsync(FridgeProduct fridgeProduct)
        {
            _context.FridgeProducts.Update(fridgeProduct);
            return Task.CompletedTask;
        }
        public async Task AddAsync(FridgeProduct fridgeProduct)
        {
            await _context.FridgeProducts.AddAsync(fridgeProduct);
        }
        public async Task DeleteAsync(int fridgeid, int productid)
        {
            var fridgeProduct = await _context.FridgeProducts
                .FirstOrDefaultAsync(x => x.FridgeId == fridgeid && x.ProductId == productid);
            if (fridgeProduct != null)
            {
                _context.FridgeProducts.Remove(fridgeProduct);
            }
        }

        public async Task SaveAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
