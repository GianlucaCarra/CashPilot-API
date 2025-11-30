namespace CashPilot.Domain.Exceptions;

public class DomainException : Exception
{
    public int StatusCode { get; }
    
    protected DomainException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}