namespace CashPilot.Domain.Entities;

public class Income
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public decimal Amount  { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}