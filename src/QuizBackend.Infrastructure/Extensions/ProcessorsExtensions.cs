using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Services.Processors.Attachments;

namespace QuizBackend.Infrastructure.Extensions;
public static class ProcessorsExtensions
{
    public static void AddProcessors(this IServiceCollection services)
    {
        services.AddSingleton<IAttachmentProcessor, AttachmentProcessor>();
    }
}
