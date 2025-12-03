using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeApp.Core.Models.Auth;

public sealed class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
