using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.ExceptionsHandlers
{
    internal sealed class UnauthorizedExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<UnauthorizedExceptionHandler> _logger;
        public UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not UnauthorizedException unauthorizedException)
            {
                return false;
            }

            _logger.LogError(
                unauthorizedException,
                "Unauthorized Exception occurred: {Message}",
                unauthorizedException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = unauthorizedException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }
    }
}
