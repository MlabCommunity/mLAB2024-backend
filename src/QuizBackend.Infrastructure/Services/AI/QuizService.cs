using QuizBackend.Application.Dtos.Quiz;
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

       public async Task<QuizDto> GenerateQuizFromPromptAsync(string topic, int numberOfQuestions, int numberOfAnswers)
        {
            var prompt = $"Generate a quiz based on the following topic: \"{topic}\". "+
                $"The quiz should contain exactly \"{numberOfQuestions}\" question and \"{numberOfAnswers}\" answers, only one answer should be correct. Respond only with JSON." +
                "Here is the required JSON format: "+
                "";
        }
    }
}
