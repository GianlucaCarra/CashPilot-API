using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Services;

public class IncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public IncomeService(IIncomeRepository incomeRepository, IMapper mapper, IUserRepository userRepository)
    {
        _incomeRepository = incomeRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ResponseAllIncomesDto> GetAllIncomes(string userId)
    {
        var incomes = await _incomeRepository.GetAllIncomesAsync(userId);
        
        return _mapper.Map<ResponseAllIncomesDto>(incomes);
    } 

    public async Task<ResponseCreateIncomeDto> CreateIncomeAsync(CreateIncomeDto dto, string userId)
    {
        var entity = _mapper.Map<Income>(dto);

        await _userRepository.FindUserByIdAsync(userId);
        
        entity.UserId = Guid.Parse(userId);
        
        await _incomeRepository.AddIncomeAsync(entity);
        await _incomeRepository.SaveAsync();

        return _mapper.Map<ResponseCreateIncomeDto>(entity);
    }
}