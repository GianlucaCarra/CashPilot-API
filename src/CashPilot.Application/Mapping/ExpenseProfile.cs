using AutoMapper;
using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Mapping;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<CreateExpenseDto, Expense>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<Expense, ResponseCreateExpenseDto>();
        CreateMap<Expense, ResponseExpenseDto>();
    }
}