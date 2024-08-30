using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI
{
    public class QuizService : IQuizService
    {
        private readonly IKernelService _kernelService;
        private readonly ILogger<QuizService> _logger;

        public QuizService(IKernelService kernelService, ILogger<QuizService> logger)
        {
            _kernelService = kernelService;
            _logger = logger;
        }
        public async Task<CreateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command)
        {
            var kernelArguments = new KernelArguments
            {
                {"content", command.Content },
                {"numberOfQuestions", command.NumberOfQuestions},
                {"typeOfQuestions", command.QuestionType}
            };

            var jsonResponse = await _kernelService.CreatePluginFromPromptDirectory("GenerateQuiz", kernelArguments);
            var quizDto = JsonConvert.DeserializeObject<CreateQuizDto>(jsonResponse);

            if (string.IsNullOrWhiteSpace(jsonResponse) || quizDto is null)
            {

                _logger.LogWarning("Quiz generation failed: Content: {Content}, NumberOfQuestions: {NumberOfQuestions}, TypeOfQuestions: {TypeOfQuestions}",
                    command.Content, command.NumberOfQuestions, command.QuestionType);

                throw new BadRequestException("Try generating again");
            }

            return quizDto;
        }
    }

}
