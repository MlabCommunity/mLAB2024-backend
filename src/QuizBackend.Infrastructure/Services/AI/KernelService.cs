using Microsoft.SemanticKernel;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI;

public class KernelService : IKernelService
{
    private readonly Kernel _kernel;

    public KernelService(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> InvokeAsync(KernelFunction kernelFunction, KernelArguments kernelArguments)
    {
        var completionResult = await _kernel.InvokeAsync(kernelFunction, kernelArguments);

        return completionResult.ToString();
    }

    public async Task<string> InvokePromptAsync(string prompt)
    {
        var completionResult = await _kernel.InvokePromptAsync(prompt);

        return completionResult.ToString();
    }

    public KernelPlugin ImportAllPlugins()
    {
        var promptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prompts");
        var prompts = _kernel.ImportPluginFromPromptDirectory(promptPath);

        return prompts;
    }
}