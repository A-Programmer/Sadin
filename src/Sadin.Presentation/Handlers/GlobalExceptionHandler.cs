using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;

namespace Sadin.Presentation.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;
 
    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }
 
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unexpected error occured");

        var statusCode = exception switch
        {
            KsNotFoundException => (int) HttpStatusCode.NotFound,
            KsValidationException => (int) HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
        
        httpContext.Response.StatusCode = statusCode;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Status = statusCode,
                Title = "An error occurred",
                Detail = exception.Message,
                Type = exception.GetType().Name,
            },
            Exception = exception
        });
    }
}