using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizBackend.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationExtension(configuration);
            services.AddMediatRExtension(configuration);
        }
    }
}