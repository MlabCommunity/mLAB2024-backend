using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.ExceptionsHandlers
{
    internal sealed class AiClientNotSupportedExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AiClientNotSupportedException> _logger;

        public AiClientNotSupportedExceptionHandler(ILogger<AiClientNotSupportedException> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not AiClientNotSupportedException aiClientNotSupportedException)
            {
                return false;
            }

            _logger.LogError(
                aiClientNotSupportedException,
                "Ai Client not supported: {Message}",
                aiClientNotSupportedException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "AI Client Not Supported",
                Detail = aiClientNotSupportedException.Message
            };

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
