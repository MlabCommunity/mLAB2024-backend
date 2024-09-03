using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.ExceptionsHandlers
{
    internal sealed class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;

        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
           
            if (exception is not ValidationException validationException)
            {
                return false;
            }

            _logger.LogError(
                validationException,
                "Validation exception occurred: {Message}",
                validationException.Message);
          
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
          
            var validationProblemDetails = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                Detail = validationException.Message
            };

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = validationProblemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken);

            return true;
        }
    }
}