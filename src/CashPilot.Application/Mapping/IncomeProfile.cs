using AutoMapper;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Mapping;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<CreateIncomeDto, Income>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore());
        CreateMap<Income, ResponseCreateIncomeDto>();
        CreateMap<Income, ResponseIncomeDto>();
    }
}