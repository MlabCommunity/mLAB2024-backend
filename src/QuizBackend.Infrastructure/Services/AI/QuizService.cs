using Microsoft.SemanticKernel;
using Newtonsoft.Json;
using QuizBackend.Application.Commands.GenerateQuiz;
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

        public async Task<CreateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command)
        {
            var kernelArguments = new KernelArguments
            {
                {"content", command.Content },
                {"numberOfQuestions", command.NumberOfQuestions},
                {"typeOfQuestions", command.TypeOfQuestions}
            };

            var jsonResponse = await _kernelService.CreatePluginFromPromptDirectory("GenerateQuiz", kernelArguments);
            var quizDto = JsonConvert.DeserializeObject<CreateQuizDto>(jsonResponse);

            return quizDto;
            
        }
    }
}
