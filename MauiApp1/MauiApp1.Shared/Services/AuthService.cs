using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace MauiApp1.Shared.Services;

public sealed class AuthService : IAuthService
{
    // später durch DB/Backend ersetzen
    private readonly ConcurrentDictionary<string, User> _users = new();

    public Task<bool> RegisterAsync(string email, string password)
    {
        email = email.Trim().ToLowerInvariant();

        var user = new User
        {
            Email = email,
            Password = password
        };

        var added = _users.TryAdd(email, user);
        return Task.FromResult(added);
    }

    public Task<User?> LoginAsync(string email, string password)
    {
        email = email.Trim().ToLowerInvariant();

        if (_users.TryGetValue(email, out var user) &&
            user.Password == password)
        {
            return Task.FromResult<User?>(user);
        }

        return Task.FromResult<User?>(null);
    }
}
