using ArcadeApp.UI;
using ArcadeApp.UI.Services;
using ArcadeApp.Mobile.Services;

namespace ArcadeApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddScoped<AuthState>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        return builder.Build();
    }
}
