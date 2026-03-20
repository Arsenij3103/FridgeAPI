using Fridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FridgeResponce> Fridges { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FridgeProduct> FridgeProducts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}