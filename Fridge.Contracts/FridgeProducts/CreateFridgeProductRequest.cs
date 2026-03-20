namespace Fridge.Contracts.FridgeProducts;

public class CreateFridgeProductRequest
{
    public int FridgeId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}