using ArcadeApp.Core.Models.Auth;
using ArcadeApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeApp.Backend.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile/{userId:guid}")]
    public async Task<ActionResult<User>> GetProfile(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userService.GetProfileAsync(userId, cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }
}
