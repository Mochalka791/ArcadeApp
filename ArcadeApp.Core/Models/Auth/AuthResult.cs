namespace ArcadeApp.Core.Models.Auth;

public sealed class AuthResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public User? User { get; set; }
}
