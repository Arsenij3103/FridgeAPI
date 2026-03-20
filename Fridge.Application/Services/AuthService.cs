using Fridge.Application.Exceptions;
using Fridge.Application.Interfaces.Repositories;
using Fridge.Application.Interfaces.Services;
using Fridge.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fridge.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(string userName, string password, string role, string email)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new BadRequestException("Username is requried");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new BadRequestException("Password is requried");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BadRequestException("Email is requried.");
        }
        if (role != "User" && role != "Manager")
        {
            throw new BadRequestException("Role must be either 'User' or 'Manager' .");
        }
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new BadRequestException("User with this email already exists.");
        }
        var user = new User
        {
            UserName = userName,
            Email = email,
            Role = role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return GenerateToken(user);
    }
    public async Task<string> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BadRequestException("Email is requried");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new BadRequestException("Password is requried");
        }
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new BadRequestException("Invaild email or password");
        }
        return GenerateToken(user);
    }
    private string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
