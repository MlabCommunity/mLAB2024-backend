using Microsoft.SemanticKernel;
using Newtonsoft.Json;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Exceptions;
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

        public async Task<GenerateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command)
        {
            var kernelArguments = new KernelArguments
            {
                {"content", command.Content },
                {"numberOfQuestions", command.NumberOfQuestions},
                {"typeOfQuestions", command.QuestionType}
            };

            var jsonResponse = await _kernelService.CreatePluginFromPromptDirectory("GenerateQuiz", kernelArguments);

            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                throw new BadRequestException("Try generate again");
            }

            var quizDto = JsonConvert.DeserializeObject<GenerateQuizDto>(jsonResponse);
            return quizDto;
        }
    }
}
