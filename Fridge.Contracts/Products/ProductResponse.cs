namespace Fridge.Contracts.Products;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultQuantity { get; set; }
}