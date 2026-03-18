using Fridge.API.Data;
using Fridge.Domain.Entities;
namespace Fridge.API.Repositories
{
    public class FridgeProductRepository : IFridgeProductRepository
    {
        private readonly AppDbContext _context;

        public FridgeProductRepository(AppDbContext context)
        {
            _context = context;
        }

       public List<FridgeProduct> GetByFridgeId(int fridgeid)
        {
            return _context.FridgeProducts
                .Where(x => x.FridgeId == fridgeid)
                .ToList();
        }
        public FridgeProduct ? Get(int fridgeid,int productid)
        {
            return _context.FridgeProducts
                .FirstOrDefault(x => x.FridgeId == fridgeid && x.ProductId == productid);
        }

        public bool Exists(int fridgeid, int productid)
        {
            return _context.FridgeProducts
                .Any(x => x.FridgeId == fridgeid && x.ProductId == productid);
        }

        public void Update(FridgeProduct fridgeProduct)
        {
            _context.FridgeProducts.Update(fridgeProduct);
        }
        public void Add(FridgeProduct fridgeProduct)
        {
            _context.FridgeProducts.Add(fridgeProduct);
        }
        public void Delete(int fridgeid, int productid)
        {
            var fridgeProduct = _context.FridgeProducts
                .FirstOrDefault(x => x.FridgeId == fridgeid && x.ProductId == productid);
            if(fridgeProduct!=null)
            {
                _context.FridgeProducts.Remove(fridgeProduct);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
