using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using QuizBackend.Application.AiConfiguration;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI
{
    public class AiClientFactory : IAiClientFactory
    {
        private readonly AiSettings _settings;

        public AiClientFactory(IOptions<AiSettings> options)
        {
            _settings = options?.Value ?? throw new ArgumentIsNullException(nameof(options), "AI settings cannot be null");
        }
        public Kernel CreateAiClient()
        {
                return _settings.Type.ToLower() switch
                {
                    "openai" => CreateOpenAIKernel(),
                    "azureopenai" => CreateAzureOpenAIKernel(),
                    "local" => CreateLocalAIKernel(),
                    _ => throw new AiClientNotSupportedException($"AI provider '{_settings.Type}' is not supported")
                };
        }

        private Kernel CreateOpenAIKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(_settings.Model, _settings.Key);
            return kernelBuilder.Build();
        }

        private Kernel CreateAzureOpenAIKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(_settings.Model, _settings.Endpoint, _settings.Key);
            return kernelBuilder.Build();
        }

        private Kernel CreateLocalAIKernel()
        {
            var endpoint = new Uri(_settings.Endpoint);
#pragma warning disable SKEXP0010 
            var kernelBuilder = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(_settings.Model, endpoint, _settings.Key);
#pragma warning restore SKEXP0010
            return kernelBuilder.Build();
        }

    }
}
