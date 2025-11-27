using System.Security.Claims;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("[controller]")]
public class OAuthController : ControllerBase
{
    [HttpGet("google")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult RedirectGoogle()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "api/login/google"
        }, "Google");
    }
    
    [HttpGet("github")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult LoginGithub()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/auth/callback"
        }, "Github");
    }
    
    [HttpGet("microsoft")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult LoginMicrosoft()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/auth/callback"
        }, "Microsoft");
    }
}