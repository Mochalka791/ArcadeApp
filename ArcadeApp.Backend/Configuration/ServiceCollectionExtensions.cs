using Microsoft.Extensions.Options;

namespace ArcadeApp.Backend.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtOptions>>().Value);
        return services;
    }
}
