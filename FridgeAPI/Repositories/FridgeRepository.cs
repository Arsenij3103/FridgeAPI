using Fridge.API.Data;
using Fridge.Core.Models;

namespace Fridge.API.Repositories
{
    public class FridgeRepository : IFridgeRepository
    {
        private readonly AppDbContext _context;
        
        public FridgeRepository(AppDbContext context)
        {
            _context = context;
        }
            
        public List<FridgeEntity> GetAll()
        {
            return _context.Fridges.ToList();
        }

        public FridgeEntity ? GetById(int id)
        {
            return _context.Fridges.FirstOrDefault(x => x.Id == id);
        }

        public void Add(FridgeEntity fridge)
        {
            _context.Fridges.Add(fridge);
        }
        
        public bool Exists(int  id)
        {
            return _context.Fridges.Any(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
