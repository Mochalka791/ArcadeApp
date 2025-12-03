using MauiApp1.Shared;
using MauiApp1.Shared.Services;

namespace MauiApp1;

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

        // <<< HIER NEU >>>
        builder.Services.AddScoped<AuthState>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        return builder.Build();
    }
}
