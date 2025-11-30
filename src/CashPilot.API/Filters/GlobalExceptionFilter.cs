using CashPilot.Domain.Exceptions;
using CashPilot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashPilot.Filters;

public class GlobalExceptionFilter : IExceptionFilter
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
        
        _logger.LogError(exception, "A new unhandled exception occurred: {Message}", exception.Message);

        ErrorResponse errorResponse;
        int statusCode;

        switch (exception)
        {
            case FluentValidation.ValidationException fluentValidationEx:
                statusCode = 400;
                var errors = fluentValidationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).Distinct().ToArray()
                    );
    
                errorResponse = new ErrorResponse(statusCode, "One or more validation errors where thrown.")
                {
                    Errors = errors
                };
                break;

            case ForbiddenException forbiddenEx:
                statusCode = forbiddenEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, forbiddenEx.Message);
                break;
            
            case BadRequestException badRequestEx:
                statusCode = badRequestEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, badRequestEx.Message);
                break;
            
            case NotFoundException notFoundEx:
                statusCode = notFoundEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, notFoundEx.Message);
                break;
            
            case ValidationException validationEx: 
                statusCode = validationEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, validationEx.Message)
                {
                    Errors = validationEx.Errors
                };
                break;
            
            case UnauthorizedException unauthorizedEx:
                statusCode = unauthorizedEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, unauthorizedEx.Message);
                break;
            
            case ConflictException conflictEx:
                statusCode = conflictEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, conflictEx.Message);
                break;
            
            case DomainException domainEx:
                statusCode = domainEx.StatusCode;
                errorResponse = new ErrorResponse(statusCode, domainEx.Message);
                break;
            
            default:
                statusCode = 500;
                var message = _environment.IsDevelopment() 
                    ? exception.Message 
                    : "Internal Server Error";
                
                errorResponse = new ErrorResponse(statusCode, message);
                break;
        }

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}