using ArcadeApp.Core.Models;
using ArcadeApp.Core.Models.Auth;
using ArcadeApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeApp.Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] UserLoginDto request, CancellationToken cancellationToken)
    {
        var user = await _authService.LoginAsync(request, cancellationToken);
        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto request, CancellationToken cancellationToken)
    {
        var user = await _authService.RegisterAsync(request, cancellationToken);
        return Ok(user);
    }
}
