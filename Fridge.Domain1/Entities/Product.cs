

namespace Fridge.Domain.Entities
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public int DefaultQuantity { get; set; } = 0;
    }
}
