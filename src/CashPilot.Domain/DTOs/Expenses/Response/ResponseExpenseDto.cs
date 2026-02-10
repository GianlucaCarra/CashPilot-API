using CashPilot.Domain.Enums.Expenses;

namespace CashPilot.Domain.DTOs.Expenses.Response;

public class ResponseExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ExpenseCategory Category { get; set; }
    public DateTime Date { get; set; }
}