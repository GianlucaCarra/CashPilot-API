namespace CashPilot.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message, 404)
    {
    }
    
    public NotFoundException(string entityName, object id)
        : base($"{entityName} with ID '{id}' was not found.", 404)
    {
    }
}