namespace ArcadeApp.UI.Services;

public sealed class AuthState
{
    public ArcadeApp.Core.Models.Auth.User? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;

    public event Action? OnChange;

    public void SignIn(ArcadeApp.Core.Models.Auth.User user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}
