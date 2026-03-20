namespace Fridge.Aplication.Interfaces.Repositories
{
    public interface IRefillRepository
    {
        Task RefillAsync(int fridgeId, int productId);
    }
}
