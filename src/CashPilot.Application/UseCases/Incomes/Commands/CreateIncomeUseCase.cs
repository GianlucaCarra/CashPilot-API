using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Services;
using CashPilot.Application.Validators.Incomes.Commands;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using FluentValidation;

namespace CashPilot.Application.UseCases.Incomes.Commands;

public class CreateIncomeUseCase
{
    private readonly IncomeService _incomeService;
    private readonly IValidator<CreateIncomeDto> _validator;

    public CreateIncomeUseCase(IncomeService incomeService, IValidator<CreateIncomeDto> validator)
    {
        _incomeService = incomeService;
        _validator = validator;
    }
    
    public async Task<ResponseCreateIncomeDto> Execute(CreateIncomeDto dto, string userId)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        return await _incomeService.CreateIncomeAsync(dto, userId);
    }
}