using ArcadeApp.Core.Models;
using ArcadeApp.Core.Services.Interfaces;

namespace ArcadeApp.Core.Services.Implementations;

public class UserService : IUserService
{
    public Task<User?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        // Placeholder profile
        var user = new User
        {
            Id = userId,
            UserName = "PlayerOne",
            Email = "player@example.com"
        };
        return Task.FromResult<User?>(user);
    }
}
