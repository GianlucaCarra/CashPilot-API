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
    private readonly CreateIncomeUseCase _createIncomeUseCase;
    private readonly GetAllExpensesUseCase _getAllExpensesUseCase;

    public IncomesController(CreateIncomeUseCase createIncomeUseCase, GetAllExpensesUseCase getAllExpensesUseCase)
    {
        _createIncomeUseCase = createIncomeUseCase;
        _getAllExpensesUseCase = getAllExpensesUseCase;
    }

    [HttpGet]
    [ProducesResponseType<List<ResponseIncomeDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllIncomesAsync()
    {
        var userId = GetUserId();
        
        if (userId is null)
            return BadRequest();
        
        var result = await _getAllExpensesUseCase.Execute(userId);
        
        if (result.Count <= 0) 
            return NoContent();
        
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType<ResponseCreateIncomeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto dto)
    {
        var userId = GetUserId();
        
        if (userId is null)
        {
            return BadRequest();
        }
        
        var result = await _createIncomeUseCase.Execute(dto, userId);
        
        return CreatedAtAction(nameof(CreateIncome), new { Id = result.Id }, result);
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}