

namespace Fridge.Application.Interfaces.Services;
public interface IAuthService
{
    Task<string> RegisterAsync(string userName, string email, string password, string role);
    Task<string> LoginAsync(string email, string password);
}
