namespace CashPilot.Domain.DTOs.Incomes.Response;

public class ResponseIncomeDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}