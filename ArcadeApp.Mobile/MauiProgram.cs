using ArcadeApp.UI.Services;
using ArcadeApp.Core.Services.Auth;

namespace ArcadeApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddScoped<AuthState>();
        builder.Services.AddScoped<IAuthService, FakeAuthService>();

        return builder.Build();
    }
}
