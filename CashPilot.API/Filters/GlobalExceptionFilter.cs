namespace CashPilot.Filters;

public class ExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionFilter(
        ILogger<GlobalExceptionFilter> logger,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        _logger.LogError(exception, "Uma exceção não tratada ocorreu: {Message}", exception.Message);

        ErrorResponse errorResponse;
        int statusCode;

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = validationEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, validationEx.Message)
                {
                    Errors = validationEx.Errors
                };
                break;

            case DomainException domainEx:
                statusCode = domainEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, domainEx.Message);
                break;

            default:
                statusCode = 500;
                var message = _environment.IsDevelopment() 
                    ? exception.Message 
                    : "Ocorreu um erro interno no servidor.";
                
                errorResponse = new ErrorResponse(statusCode, message);
                
                if (_environment.IsDevelopment())
                {
                    errorResponse.StackTrace = exception.StackTrace;
                }
                break;
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}