using System.Security.Claims;
using CashPilot.Application.UseCases.Expenses.Commands;
using CashPilot.Application.UseCases.Incomes.Queries;
using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly CreateExpenseUseCase _createExpenseUseCase;
    private readonly GetAllExpensesUseCase _getAllExpensesUseCase;
    public ExpensesController(
        CreateExpenseUseCase createExpenseUseCase,
        GetAllExpensesUseCase getAllExpensesUseCase)
    {
        _createExpenseUseCase = createExpenseUseCase;
        _getAllExpensesUseCase = getAllExpensesUseCase;
    }
    
    [HttpGet]
    [ProducesResponseType<List<ResponseExpenseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpensesAsync()
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
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto dto)
    {
        var userId = GetUserId();
        
        if (userId is null)
        {
            return BadRequest();
        }
        
        var result = await _createExpenseUseCase.Execute(dto, userId);
        
        return CreatedAtAction(nameof(CreateExpense), new { Id = result.Id }, result);
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}