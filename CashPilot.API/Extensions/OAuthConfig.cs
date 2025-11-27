using CashPilot.Application.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CashPilot.Extensions;

public static class OAuthConfig
{
    public static AuthenticationBuilder AddOAuthClients(
        this AuthenticationBuilder authentication, 
        ConfigurationManager configuration)
    {
        var settings =  configuration.GetSection("OAuth").Get<OAuthSettings>();
        
        if (settings is null)
        {
            throw new Exception("Configuration section Not Found.");
        }
        
        authentication
            .AddGoogle("Google", options =>
            {
                options.SignInScheme = "ExternalCookie";
                options.ClientId = settings.Google.ClientId;
                options.ClientSecret = settings.Google.ClientSecret;
                options.CallbackPath = "/signin-google";

                options.SaveTokens = true;
                
                options.Scope.Add("email");
                options.Scope.Add("profile");
            })
            .AddOAuth("Github", options =>
            {
                options.SignInScheme = "ExternalCookie";
                options.ClientId = settings.GitHub.ClientId;
                options.ClientSecret = settings.GitHub.ClientSecret;
                options.CallbackPath = settings.GitHub.CallbackPath;
                
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";
                
                options.SaveTokens = true;
                
            })
            .AddMicrosoftAccount("Microsoft", options =>
            {
                options.SignInScheme = "ExternalCookie";
                options.ClientId = settings.Microsoft.ClientId;
                options.ClientSecret = settings.Microsoft.ClientSecret;
                options.CallbackPath = settings.Microsoft.CallbackPath;
                
                options.AuthorizationEndpoint = "https://microsoft-identity.github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://microsoft-identity.github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";
                
                options.SaveTokens = true;
            });
        
        return authentication;
    }
}