namespace CashPilot.Domain.DTOs.Logins.Response;

public class ResponseCreateLoginDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}