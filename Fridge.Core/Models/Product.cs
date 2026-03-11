

namespace Fridge.Core.Models
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public int DefaultQuantity { get; set; } = 0;
    }
}
