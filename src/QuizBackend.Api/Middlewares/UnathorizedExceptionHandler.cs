using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Api.Middlewares
{
    internal sealed class UnathorizedExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<UnathorizedExceptionHandler> _logger;
        public UnathorizedExceptionHandler(ILogger<UnathorizedExceptionHandler> logger)
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
