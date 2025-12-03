namespace ArcadeApp.Core.Models.Auth;

public sealed class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;
}
