using CashPilot.Domain.Entities;

namespace CashPilot.Domain.DTOs.Incomes.Response;

public class ResponseAllIncomesDto
{
    public List<Income> Incomes { get; set; }
}