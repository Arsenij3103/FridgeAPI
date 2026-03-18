using Fridge.Application.Exceptions;
using Fridge.Application.Interfaces.Services;
using Fridge.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;


namespace Fridge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var token = await _authService.RegisterAsync(
                request.UserName,
                request.Email,
                request.Password,
                request.Role);
            var response = new AuthResponse
            {
                Token = token
            };
            return Ok(response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(
                request.Email,
                request.Password);
            return Ok(new AuthResponse
            {
                Token = token
            });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
