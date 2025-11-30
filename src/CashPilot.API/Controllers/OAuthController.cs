using System.Security.Claims;
using System.Text.Json;
using CashPilot.Application.Services;
using CashPilot.Application.UseCases.OAuth.Commands;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;
using CashPilot.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("[controller]")]
public class OAuthController : ControllerBase
{
    private readonly LogOrCreateGoogleUserUseCase _googleUserUseCase;
    public OAuthController(LogOrCreateGoogleUserUseCase googleUserUseCase)
    {
        _googleUserUseCase = googleUserUseCase;
    }
    
    [HttpGet("google")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult RedirectGoogle()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/oauth/callback/google"
        }, "Google");
    }
    
    [HttpGet("callback/google")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginGoogle()
    {
        var authResult = await HttpContext.AuthenticateAsync("ExternalCookie");
        
        if (!authResult.Succeeded)
        {
            throw new BadRequestException("External authentication error");
        }

        var response = await _googleUserUseCase.Execute(authResult.Principal);
        
        return Created(nameof(LoginGoogle), response);
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