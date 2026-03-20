namespace Fridge.Contracts.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultQuantity { get; set; }
    }
}
