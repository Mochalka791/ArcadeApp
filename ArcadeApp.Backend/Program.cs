using ArcadeApp.Backend.Configuration;
using ArcadeApp.Backend.Mapping;
using ArcadeApp.Backend.Middlewares;
using ArcadeApp.Core.Services.Implementations;
using ArcadeApp.Core.Services.Interfaces;
using ArcadeApp.UI.State;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ArcadeApp.Core.Services.Interfaces.IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatsService, StatsService>();

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthState>();

builder.Services.AddScoped<
    ArcadeApp.Core.Services.Auth.IAuthService,
    ArcadeApp.Core.Services.Auth.FakeAuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAntiforgery();

app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.MapRazorComponents<ArcadeApp.UI.App>()
   .AddInteractiveServerRenderMode();

app.Run();
