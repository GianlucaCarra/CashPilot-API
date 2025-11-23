namespace CashPilot.Domain.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message = "Access forbidden.") : base(message, 403)
    {}    
}