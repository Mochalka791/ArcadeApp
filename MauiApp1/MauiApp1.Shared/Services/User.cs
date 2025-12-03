using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.Shared.Services;

public sealed class User
{
    public string Email { get; set; } = string.Empty;
    // später -> verschlüsseln
    public string Password { get; set; } = string.Empty;
}
