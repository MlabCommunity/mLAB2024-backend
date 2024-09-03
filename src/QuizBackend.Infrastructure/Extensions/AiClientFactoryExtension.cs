using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.AiConfiguration;
using QuizBackend.Infrastructure.Interfaces;
using QuizBackend.Infrastructure.Services.AI;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class AiClientFactoryExtension
    {
        public static void AddAiClientFactoryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AiSettings>(configuration.GetSection("AiSettings"));
            services.AddSingleton<IAiClientFactory, AiClientFactory>();
        }
    }
}