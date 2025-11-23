namespace CashPilot.Domain.Exceptions;

public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message = "Internal conflict.", 409)
    {}
}