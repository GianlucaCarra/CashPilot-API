namespace CashPilot.Domain.DTOs.Expenses.Response;

public class ResponseCreateExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}