
namespace Fridge.Domain.Entities
{
    public class FridgeProduct
    {
        public int Id { get; set; }
        public int FridgeId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
