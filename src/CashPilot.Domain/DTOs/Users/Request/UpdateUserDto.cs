namespace CashPilot.Domain.DTOs.Users.Request;

public class UpdateUserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
}