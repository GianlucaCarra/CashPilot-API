namespace CashPilot.Domain.Exceptions;

public class ValidationException : DomainException 
{
    public Dictionary<string, string[]>? Errors { get; }

    public ValidationException(string message) : base(message)
    {
        Errors = new();
    }
    
    public ValidationException(string message, Dictionary<string, string[]> errors) 
        : base("One or more validation errors where thrown.")
    {}
}