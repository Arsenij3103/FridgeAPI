
using Fridge.Application.Interfaces.Repositories;
using Fridge.Domain.Entities;
using Fridge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
