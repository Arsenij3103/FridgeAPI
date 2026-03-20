using Fridge.Domain.Entities;

namespace Fridge.Application.Interfaces.Repositories;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveAsync();

}
