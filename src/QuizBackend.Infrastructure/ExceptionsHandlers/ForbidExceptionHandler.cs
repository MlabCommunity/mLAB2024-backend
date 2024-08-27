using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.ExceptionsHandlers
{
    internal sealed class ForbidExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ForbidExceptionHandler> _logger;
        public ForbidExceptionHandler(ILogger<ForbidExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ForbidException ForbidException)
            {
                return false;
            }

            _logger.LogError(
                ForbidException,
                "Forbid Exception occurred: {Message}",
                ForbidException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbid",
                Detail = "You have not permission for that action"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }
    }
}
