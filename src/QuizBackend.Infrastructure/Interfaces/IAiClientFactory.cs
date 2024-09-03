using Microsoft.SemanticKernel;

namespace QuizBackend.Infrastructure.Interfaces
{
    public interface IAiClientFactory
    {
        Kernel CreateAiClient();
    }
}