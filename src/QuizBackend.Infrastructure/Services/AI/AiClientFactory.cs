using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using QuizBackend.Application.AiConfiguration;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;
using QuizBackend.Infrastructure.Services.Delegating;
using System.Reflection;

namespace QuizBackend.Infrastructure.Services.AI
{
    public class AiClientFactory : IAiClientFactory
    {
        private readonly AiSettings _settings;

        public AiClientFactory(IOptions<AiSettings> options)
        {
            _settings = options?.Value ?? throw new ArgumentIsNullException(nameof(options), "AI settings cannot be null");
            ValidateSettings(_settings);
        }

        private void ValidateSettings(AiSettings settings)
        {
            var properties = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var value = property.GetValue(settings) as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentIsNullException(property.Name);
                }
            }
        }
        public Kernel CreateAiClient()
        {
                return _settings.Type.ToLower() switch
                {
                    "openai" => CreateOpenAIKernel(),
                    "azureopenai" => CreateAzureOpenAIKernel(),
                    "local" => CreateLocalAIKernel(),
                    "groq" => CreateGroqAIKernel(),
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

        private Kernel CreateGroqAIKernel()
        {
            HttpClient httpClient = new(new GroqDelegatingHandler(_settings.Endpoint));
            var kernelBuilder = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(_settings.Model, _settings.Key, httpClient: httpClient);
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
