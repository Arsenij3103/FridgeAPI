using Fridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fridge.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FridgeEntity> Fridges { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FridgeProduct> FridgeProducts { get; set; }
    }
}