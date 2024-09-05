using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Infrastructure.ExceptionsHandlers;

namespace QuizBackend.Infrastructure.Extensions;

public static class ExceptionHandlersExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }
}