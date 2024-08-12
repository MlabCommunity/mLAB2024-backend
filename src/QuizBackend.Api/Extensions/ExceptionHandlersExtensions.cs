using QuizBackend.Api.Middlewares;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Api.Extensions
{
    public static class ExceptionHandlersExtensions
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services) 
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<UnathorizedExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            

            services.AddProblemDetails();

            return services;
        }
    }
}
