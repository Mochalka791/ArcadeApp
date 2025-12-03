using System.Threading;
using System.Threading.Tasks;

namespace ArcadeApp.Core.Services.Auth;

public interface IAuthService
{
    Task<ArcadeApp.Core.Models.Auth.AuthResult> RegisterAsync(
        ArcadeApp.Core.Models.Auth.RegisterRequest request,
        CancellationToken ct = default);

    Task<ArcadeApp.Core.Models.Auth.AuthResult> LoginAsync(
        ArcadeApp.Core.Models.Auth.LoginRequest request,
        CancellationToken ct = default);
}
