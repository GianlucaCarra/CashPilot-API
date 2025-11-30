using CashPilot.Domain.Entities;

namespace CashPilot.Domain.DTOs.Incomes.Response;

public class ResponseAllIncomesDto
{
    public List<ResponseIncomeDto> Incomes { get; set; }
}