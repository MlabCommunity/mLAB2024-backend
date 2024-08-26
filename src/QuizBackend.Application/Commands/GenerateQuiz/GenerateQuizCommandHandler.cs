using QuizBackend.Application.Dtos.Quiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.GenerateQuiz
{
    public class GenerateQuizCommandHandler : ICommandHandler<GenerateQuizCommand, GenerateQuizDto>
    {
        private readonly IQuizService _quizService;

        public GenerateQuizCommandHandler(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public async Task<GenerateQuizDto> Handle(GenerateQuizCommand command, CancellationToken cancellationToken)
        {
            var quizDto = await _quizService.GenerateQuizFromPromptTemplateAsync(command);

            return quizDto;
        }
    }
}
