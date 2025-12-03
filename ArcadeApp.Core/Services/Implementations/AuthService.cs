using ArcadeApp.Core.Models;
using ArcadeApp.Core.Models.Auth;
using ArcadeApp.Core.Services.Interfaces;

namespace ArcadeApp.Core.Services.Implementations;

public class AuthService : IAuthService
{
    public Task<User?> LoginAsync(UserLoginDto request, CancellationToken cancellationToken = default)
    {
        // Stubbed login for architecture scaffolding
        var user = new User
        {
            Email = request.Email,
            UserName = request.Email.Split('@').FirstOrDefault() ?? "user"
        };
        return Task.FromResult<User?>(user);
    }

    public Task<User> RegisterAsync(UserRegisterDto request, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName
        };
        return Task.FromResult(user);
    }
}
