using Fridge.Aplication.Interfaces.Repositories;
using Fridge.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure.Repositories
{
    public class RefillRepository : IRefillRepository
    {
        private readonly AppDbContext _context;
        public RefillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task RefillAsync(int fridgeId, int productId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                 "EXEC RefillProduct @FridgeId,ProductId",
                 new SqlParameter("@FridgeId", fridgeId),
                 new SqlParameter("@ProductId", productId));
        }
    }
}
