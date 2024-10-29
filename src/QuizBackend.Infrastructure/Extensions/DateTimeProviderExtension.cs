using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Time;

namespace QuizBackend.Infrastructure.Extensions;

public static class DateTimeProviderExtension
{
    public static void AddDateTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}