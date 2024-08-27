using Microsoft.SemanticKernel;
using Newtonsoft.Json.Linq;

namespace QuizBackend.Infrastructure.Interfaces
{
    public interface IKernelService
    {
        Task<string> InvokePromptAsync(string prompt);
        Task<string> CreatePluginFromPromptDirectory(string promptKey, KernelArguments kernelArguments);
    }
}
