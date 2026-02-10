using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Validators.Expenses.Commands;
using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;
using FluentValidation;

namespace CashPilot.Application.UseCases.Expenses.Commands;

public class CreateExpenseUseCase
{
    private readonly IExpenseService _expenseService;
    private readonly IValidator<CreateExpenseDto> _validator;

    public CreateExpenseUseCase(
        IExpenseService expenseService,
        IValidator<CreateExpenseDto> validator)
    {
        _expenseService = expenseService;
        _validator = validator;
    }

    public async Task<ResponseCreateExpenseDto> Execute(CreateExpenseDto dto, string userId)
    {
        var result = await _validator.ValidateAsync(dto);
        
        if (!result.IsValid) throw new ValidationException(result.Errors);

        return await _expenseService.CreateExpenseAsync(dto, userId);
    }
}