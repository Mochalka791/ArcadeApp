using ArcadeApp.Backend.Configuration;
using ArcadeApp.Backend.Mapping;
using ArcadeApp.Backend.Middlewares;
using ArcadeApp.Core.Services.Implementations;
using ArcadeApp.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatsService, StatsService>();

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
