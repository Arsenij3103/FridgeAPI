using Fridge.API.Data;
using Microsoft.EntityFrameworkCore;
namespace Fridge.API.Repositories
{
    public class RefillRepository : IRefillRepository
    {
        private readonly AppDbContext _context;
        public RefillRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Refill()
        {
            _context.Database.ExecuteSqlRaw("EXEC Refill");
        }
    }
}
