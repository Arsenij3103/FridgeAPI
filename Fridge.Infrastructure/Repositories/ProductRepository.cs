using Fridge.Domain.Entities;
using Fridge.API.Data;
using System.Runtime.CompilerServices;
namespace Fridge.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product ? GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var product =_context.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
