using CashPilot.Application.UseCases.Logins.Commands;
using CashPilot.Domain.DTOs.Logins.Request;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly CreateLoginUseCase _loginUseCase;
    private readonly IConfiguration _configuration;
    private readonly ForgotPasswordUseCase _forgotPasswordUseCase;

    public LoginController(
        CreateLoginUseCase loginUseCase,
        IConfiguration configuration,
        ForgotPasswordUseCase forgotPasswordUseCase
        )
    {
        _loginUseCase = loginUseCase;
        _configuration = configuration;
        _forgotPasswordUseCase = forgotPasswordUseCase;
    }
    
    [HttpPost]
    [ProducesResponseType<ResponseCreateLoginDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] CreateLoginDto dto)
    {
        var response = await _loginUseCase.Execute(dto.Email, dto.Password);
        
        SetAuthCookies(response.Token);
        
        return Ok(response);
    }
    
    [HttpGet("login/google")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginGoogle()
    {
        // var authResult = await HttpContext.AuthenticateAsync("ExternalCookie");
        //
        // if (!authResult.Succeeded)
        // {
        //     throw new BadRequestException("External authentication error");
        // }
        
        // var claims = authResult.Principal.Claims.ToList();
        //
        // var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        // var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        
        return Created("", null);
    }
    
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        
        return Ok();
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        await _forgotPasswordUseCase.Execute(dto);
        
        return Ok();
    }

    private void SetAuthCookies(string token)
    {
        var expirationMinutes = int.Parse(
            _configuration["JwtSettings:ExpirationMinutes"] ?? "60"
        );

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SameSite = SameSiteMode.Strict
        };
        
        Response.Cookies.Append("AuthToken", token, cookieOptions);
    }
}