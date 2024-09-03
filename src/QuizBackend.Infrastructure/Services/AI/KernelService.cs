using Microsoft.SemanticKernel;
using Newtonsoft.Json.Linq;
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

        public async Task<string> CreatePluginFromPromptDirectory(string promptKey, KernelArguments kernelArguments)
        {
            var promptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prompts");
            var prompts = _kernel.ImportPluginFromPromptDirectory(promptPath);
            var promptConfig = await LoadPromptConfigAsync(Path.Combine(promptPath, promptKey, "config.json"));
            var prompt = prompts[promptKey];

            var inputVariables = promptConfig["input_variables"] as JArray;

            foreach(var inputVariable in inputVariables)
            {
                var name = inputVariable["name"]?.ToString();
                var required = inputVariable["required"]?.ToObject<bool>() ?? false;
            }

            var result = await _kernel.InvokeAsync<string>(prompt, kernelArguments);

            return result;
        }

        public async Task<JObject> LoadPromptConfigAsync(string filePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            var json = await File.ReadAllTextAsync(fullPath);
            return JObject.Parse(json);
        }
    }
}