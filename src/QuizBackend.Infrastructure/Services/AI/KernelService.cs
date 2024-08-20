using Microsoft.SemanticKernel;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI
{
    public class KernelService : IKernelService
    {
        private readonly Kernel _kernel;

        public KernelService(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<string> InvokePromptAsync(string prompt)
        {
            var completionResult = await _kernel.InvokePromptAsync(prompt);

            return completionResult.ToString();
        }
    }
}
