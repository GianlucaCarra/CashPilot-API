using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public IncomeService(
        IIncomeRepository incomeRepository, 
        IUserRepository userRepository,
        IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseIncomeDto>> GetAllIncomes(string userId)
    {
        var incomes = await _incomeRepository.GetAllIncomesAsync(userId);

        return _mapper.Map<List<ResponseIncomeDto>>(incomes);
    }

    public async Task<ResponseCreateIncomeDto> CreateIncomeAsync(CreateIncomeDto dto, string userId)
    {
        var incomeEntity = _mapper.Map<Income>(dto);
        
        incomeEntity.UserId = Guid.Parse(userId);
        if (!string.IsNullOrEmpty(dto.Description)) incomeEntity.Description = dto.Description;
        
        await _incomeRepository.AddIncomeAsync(incomeEntity);
        await _incomeRepository.SaveAsync();

        return _mapper.Map<ResponseCreateIncomeDto>(incomeEntity);
    }
}