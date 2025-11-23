using System.Text;
using CashPilot.Application.Configuration;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CashPilot.Extensions;

public static class AuthConfig
{
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings") );
        
        var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        if (settings is null)
        {
            throw new Exception("Configuration section Not Found.");
        }
        
        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(settings.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Headers.Authorization
                        .FirstOrDefault()?.Split(" ").Last();

                    if (string.IsNullOrEmpty(token))
                    {
                        token = context.Request.Cookies["AuthToken"];
                    }

                    context.Token = token;
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();
        
        return services;
    }
}