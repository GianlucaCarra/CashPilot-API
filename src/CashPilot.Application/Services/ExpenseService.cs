using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ResponseExpenseDto>> GetAllExpenses(string userId)
    {
        var expenses = await _expenseRepository.GetAllExpensesAsync(userId);
        
        return _mapper.Map<List<ResponseExpenseDto>>(expenses);
    }

    public async Task<ResponseCreateExpenseDto> CreateExpenseAsync(CreateExpenseDto dto, string userId)
    {
        var expenseEntity = _mapper.Map<Expense>(dto);
        
        expenseEntity.UserId = Guid.Parse(userId);
        
        await _expenseRepository.AddExpenseAsync(expenseEntity);
        await _expenseRepository.SaveAsync();
        
        return _mapper.Map<ResponseCreateExpenseDto>(expenseEntity);
    }
}