using System.Text;
using CashPilot.Application.Configuration;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;

namespace CashPilot.Extensions;

public static class AuthConfig
{
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<OAuthSettings>(configuration.GetSection("OAuth"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings") );

        var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var clientSettings =  configuration.GetSection("OAuth").Get<OAuthSettings>();
        
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | 
                                       ForwardedHeaders.XForwardedProto | 
                                       ForwardedHeaders.XForwardedHost;
            
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });
        
        if (settings is null)
        {
            throw new Exception("Configuration section Not Found.");
        }
        
        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultSignInScheme = "ExternalCookie";
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
            })
            .AddCookie("ExternalCookie", options =>
            {
                options.Cookie.Name = "CashPilot.External";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            // .AddOAuthClients(configuration);
            .AddGoogle(options =>
            {
                options.SignInScheme = "ExternalCookie";
                options.ClientId = clientSettings!.Google.ClientId;
                options.ClientSecret = clientSettings.Google.ClientSecret;
                options.CallbackPath = clientSettings.Google.CallbackPath;
                
                options.SaveTokens = true;
                
                options.Scope.Add("email");
                options.Scope.Add("profile");
            });
        services.AddAuthorization();
        
        return services;
    }
}