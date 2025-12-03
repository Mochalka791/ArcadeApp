using ArcadeApp.Core.Models.Auth;

namespace ArcadeApp.Core.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
}
