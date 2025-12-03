using ArcadeApp.Backend.Configuration;
using ArcadeApp.Backend.Mapping;
using ArcadeApp.Backend.Middlewares;
using ArcadeApp.Core.Services.Implementations;
using ArcadeApp.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// === API-Teil ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatsService, StatsService>();

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

// === UI-Teil ===
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Auth-State der UI
builder.Services.AddScoped<ArcadeApp.UI.State.AuthState>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

// API
app.MapControllers();

// UI – App aus ArcadeApp.UI hosten
app.MapRazorComponents<ArcadeApp.UI.App>()
   .AddInteractiveServerRenderMode();

app.Run();
