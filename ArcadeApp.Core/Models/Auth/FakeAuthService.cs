using ArcadeApp.Core.Models.Auth;

namespace ArcadeApp.Core.Services.Auth;

public sealed class FakeAuthService : IAuthService
{
    private readonly Dictionary<string, (User User, string Password)> _users = new();

    public Task<AuthResult> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        if (_users.ContainsKey(request.Email))
        {
            return Task.FromResult(new AuthResult
            {
                Success = false,
                Error = "Benutzer existiert bereits"
            });
        }

        var user = new User { Email = request.Email };
        _users[request.Email] = (user, request.Password);

        return Task.FromResult(new AuthResult
        {
            Success = true,
            User = user
        });
    }

    public Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        if (_users.TryGetValue(request.Email, out var entry) &&
            entry.Password == request.Password)
        {
            return Task.FromResult(new AuthResult
            {
                Success = true,
                User = entry.User
            });
        }

        return Task.FromResult(new AuthResult
        {
            Success = false,
            Error = "Falsche Zugangsdaten"
        });
    }
}
