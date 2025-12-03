using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.Shared.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(string email, string password);
    Task<User?> LoginAsync(string email, string password);
}
