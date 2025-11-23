using CashPilot.Application.UseCases.Validations.Commands;
using CashPilot.Domain.DTOs.Verifications.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerificationController : ControllerBase
{
    private readonly ValidateEmailUseCase _validateEmailUseCase;
    private readonly ResendValidationEmailUseCase _resendValidationEmailUseCase;

    public VerificationController(ValidateEmailUseCase validateEmailUseCase, ResendValidationEmailUseCase resendValidationEmailUseCase)
    {
        _validateEmailUseCase = validateEmailUseCase;
        _resendValidationEmailUseCase = resendValidationEmailUseCase;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateEmail([FromQuery] string token)
    {
        await _validateEmailUseCase.Execute(token);
        
        return Ok();
    }

    [HttpPost("resend-validation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResendVerificationEmail([FromBody] ResendValidationEmailDto dto)
    {
        await _resendValidationEmailUseCase.Execute(dto);
        
        return Ok();
    }
}