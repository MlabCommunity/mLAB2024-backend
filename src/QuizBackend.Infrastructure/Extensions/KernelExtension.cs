using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Infrastructure.Interfaces;
using QuizBackend.Infrastructure.Services.AI;

namespace QuizBackend.Infrastructure.Extensions;

public static class KernelExtension
{
    public static void AddKernelExtension(this IServiceCollection services)
    {
        services.AddScoped<IKernelService, KernelService>(sp =>
        {
            var factory = sp.GetRequiredService<IAiClientFactory>();
            var aiClient = factory.CreateAiClient();
            return new KernelService(aiClient);
        });
    }
}