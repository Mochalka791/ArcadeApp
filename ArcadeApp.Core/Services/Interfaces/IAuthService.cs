using ArcadeApp.Core.Models;

namespace ArcadeApp.Core.Services.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(UserLoginDto request, CancellationToken cancellationToken = default);
    Task<User> RegisterAsync(UserRegisterDto request, CancellationToken cancellationToken = default);
}
