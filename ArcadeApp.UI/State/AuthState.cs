using ArcadeApp.Core.Models.Auth;


namespace ArcadeApp.UI.State;

/// <summary>
/// Client-side authentication snapshot for the UI layer.
/// </summary>
public sealed class AuthState
{
    public User? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser is not null;

    public event Action? OnChange;

    public void SignIn(User user)
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
