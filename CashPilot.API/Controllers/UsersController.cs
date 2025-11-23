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

    public UsersController(
        CreateUserUseCase createUserUseCase, 
        UpdateUserUseCase updateUserUseCase)
    {
        _createUserUseCase =  createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
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
