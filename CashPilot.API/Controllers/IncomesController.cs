using System.Security.Claims;
using CashPilot.Application.UseCases.Incomes.Commands;
using CashPilot.Application.UseCases.Incomes.Queries;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IncomesController : ControllerBase
{
    private readonly string? _userId;
    private readonly CreateIncomeUseCase _createIncomeUseCase;
    private readonly GetAllIncomesUseCase _getAllIncomesUseCase;

    public IncomesController(CreateIncomeUseCase createIncomeUseCase, GetAllIncomesUseCase getAllIncomesUseCase)
    {
        _createIncomeUseCase = createIncomeUseCase;
        _getAllIncomesUseCase = getAllIncomesUseCase;
        _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [HttpGet]
    [ProducesResponseType<ResponseAllIncomesDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetAllIncomesAsync(string userId)
    {
        if (_userId is null)
        {
            return BadRequest();
        }
        
        var result = await _getAllIncomesUseCase.Execute(userId);
        
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType<ResponseCreateIncomeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto dto)
    {
        if (_userId is null)
        {
            return BadRequest();
        }
        
        var result = await _createIncomeUseCase.Execute(dto, _userId);
        
        return CreatedAtAction(nameof(CreateIncome), new { Id = result.Id }, result);
    }
    
    
}