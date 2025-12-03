namespace CashPilot.Domain.DTOs.Incomes.Request;

public class CreateIncomeDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; } 
}