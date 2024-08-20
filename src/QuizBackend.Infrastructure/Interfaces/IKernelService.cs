
namespace QuizBackend.Infrastructure.Interfaces
{
    public interface IKernelService
    {
        Task<string> InvokePromptAsync(string prompt);
    }
}
