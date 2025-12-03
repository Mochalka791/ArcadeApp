using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ArcadeApp.Backend.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
        if (!string.IsNullOrWhiteSpace(token))
        {
            _ = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder"))
            }, out _);
        }

        await _next(context);
    }
}


