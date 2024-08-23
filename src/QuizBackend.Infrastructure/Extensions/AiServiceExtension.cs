using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Interfaces;
using QuizBackend.Infrastructure.Services.AI;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class AiServiceExtension
    {
        public static void AddAiServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IQuizService, QuizService>();
        }
    }
}
