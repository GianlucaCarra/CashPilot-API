namespace CashPilot.Application.Interfaces.Helpers;

public interface IEmailHelper
{
    Task EmailExists(string email);
}