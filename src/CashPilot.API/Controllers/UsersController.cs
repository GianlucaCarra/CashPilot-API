using CashPilot.Application.UseCases.Logins.Commands;
using CashPilot.Application.UseCases.Users.Commands;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly ResetPasswordUseCase _resetPasswordUseCase;

    public UsersController(
        CreateUserUseCase createUserUseCase, 
        UpdateUserUseCase updateUserUseCase, ResetPasswordUseCase resetPasswordUseCase)
    {
        _createUserUseCase =  createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _resetPasswordUseCase = resetPasswordUseCase;
    }
    

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _createUserUseCase.Execute(dto);
        
        return CreatedAtAction(nameof(CreateUser), new { Id = result.Id }, result);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto, [FromQuery] string token)
    {
        await _resetPasswordUseCase.Execute(dto, token);
        
        return Ok();
    }

    [Authorize]
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateUserById([FromRoute] string id, UpdateUserDto dto)
    {
        await _updateUserUseCase.Execute(dto, id);
        
        return NoContent();
    }
}
