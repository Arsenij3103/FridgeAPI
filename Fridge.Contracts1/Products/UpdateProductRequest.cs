namespace Fridge.Contracts.Products
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultQuantity { get; set; }
    }
}
