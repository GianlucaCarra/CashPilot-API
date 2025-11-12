using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.UseCases.Users.Commands;
using CashPilot.Domain.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;

    public UsersController(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase =  createUserUseCase;
    }
    
    // [HttpGet("{id:guid}")]
    // public IActionResult GetUserById(Guid id)
    // {
    // }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _createUserUseCase.Execute(dto);
        
        return Ok(result);
    }
}