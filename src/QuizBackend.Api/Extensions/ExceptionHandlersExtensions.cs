using QuizBackend.Api.Middlewares;

namespace QuizBackend.Api.Extensions
{
    public static class ExceptionHandlersExtensions
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services) 
        { 
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
