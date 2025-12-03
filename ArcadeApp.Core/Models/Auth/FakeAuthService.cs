using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArcadeApp.Core.Services.Auth;

public sealed class FakeAuthService : ArcadeApp.Core.Services.Auth.IAuthService
{
    private readonly Dictionary<string, (ArcadeApp.Core.Models.User User, string Password)> _users
        = new(StringComparer.OrdinalIgnoreCase);

    public Task<ArcadeApp.Core.Models.Auth.AuthResult> RegisterAsync(
        ArcadeApp.Core.Models.Auth.RegisterRequest request,
        CancellationToken ct = default)
    {
        if (_users.ContainsKey(request.Email))
        {
            return Task.FromResult(new ArcadeApp.Core.Models.Auth.AuthResult
            {
                Success = false,
                Error = "Benutzer existiert bereits."
            });
        }

        var user = new ArcadeApp.Core.Models.User
        {
            Email = request.Email
        };

        _users[request.Email] = (user, request.Password);

        return Task.FromResult(new ArcadeApp.Core.Models.Auth.AuthResult
        {
            Success = true,
            User = user
        });
    }

    public Task<ArcadeApp.Core.Models.Auth.AuthResult> LoginAsync(
        ArcadeApp.Core.Models.Auth.LoginRequest request,
        CancellationToken ct = default)
    {
        if (_users.TryGetValue(request.Email, out var entry) &&
            entry.Password == request.Password)
        {
            return Task.FromResult(new ArcadeApp.Core.Models.Auth.AuthResult
            {
                Success = true,
                User = entry.User
            });
        }

        return Task.FromResult(new ArcadeApp.Core.Models.Auth.AuthResult
        {
            Success = false,
            Error = "Falsche Zugangsdaten."
        });
    }
}
