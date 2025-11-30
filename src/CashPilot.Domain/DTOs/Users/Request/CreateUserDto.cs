namespace CashPilot.Domain.DTOs.Users.Request;

public class CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public string Password { get; set; }  = string.Empty;
}