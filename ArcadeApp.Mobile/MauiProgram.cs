namespace ArcadeApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddScoped<ArcadeApp.UI.Services.AuthState>();
        builder.Services.AddScoped<
            ArcadeApp.Core.Services.Auth.IAuthService,
            ArcadeApp.Core.Services.Auth.FakeAuthService>();

        return builder.Build();
    }
}
