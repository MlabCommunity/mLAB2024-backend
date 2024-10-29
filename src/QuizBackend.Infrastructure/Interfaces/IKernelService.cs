using Microsoft.SemanticKernel;

namespace QuizBackend.Infrastructure.Interfaces;

public interface IKernelService
{
    Task<string> InvokePromptAsync(string prompt);
    Task<string> InvokeAsync(KernelFunction kernelFunction, KernelArguments kernelArguments);
    KernelPlugin ImportAllPlugins();
}