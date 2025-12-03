using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.Shared.Services;

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
