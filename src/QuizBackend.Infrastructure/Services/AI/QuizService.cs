using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI
{
    public class QuizService : IQuizService
    {
        private readonly IKernelService _kernelService;

        public QuizService(IKernelService kernelService)
        {
            _kernelService = kernelService;
        }

        public async Task<string> GetTextFromPromptAsync(string prompt)
        {
           var response = await _kernelService.InvokePromptAsync(prompt);

            return response;

        }

    }
}
