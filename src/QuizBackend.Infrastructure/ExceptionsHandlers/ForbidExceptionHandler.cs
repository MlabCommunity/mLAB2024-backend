using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.ExceptionsHandlers;
internal sealed class ForbidExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ForbidExceptionHandler> _logger;

    public ForbidExceptionHandler(ILogger<ForbidExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ForbidException forbidException)
        {
            return false;
        }

        _logger.LogError(
          forbidException,
          "Forbidden access occurred: {Message}",
          forbidException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Detail = forbidException.Message,
            Extensions =
            {
                { "resource", forbidException.ResourceName },
                { "action", forbidException.ActionAttempted },
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
