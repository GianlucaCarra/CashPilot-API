using System.Security.Claims;
using CashPilot.Application.UseCases.Incomes.Commands;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IncomesController : ControllerBase
{
    private readonly CreateIncomeUseCase _createIncomeUseCase;

    public IncomesController(CreateIncomeUseCase createIncomeUseCase)
    {
        _createIncomeUseCase = createIncomeUseCase;
    }
    
    [HttpPost]
    [ProducesResponseType<ResponseCreateIncomeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId is null)
        {
            return BadRequest();
        }
        
        var result = await _createIncomeUseCase.Execute(dto, userId);
        
        return CreatedAtAction(nameof(CreateIncome), new { Id = result.Id }, result);
    }
    
    
}