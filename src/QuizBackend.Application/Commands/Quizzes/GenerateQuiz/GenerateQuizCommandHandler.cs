using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public record GenerateQuizResponse(string Title, string Description, List<GenerateQuestion> GenerateQuestions);
    public record GenerateQuestion(string Title, List<GenerateAnswer> GenerateAnswers);
    public record GenerateAnswer(string Content, bool IsCorrect);
    public class GenerateQuizCommandHandler : ICommandHandler<GenerateQuizCommand, GenerateQuizResponse>
    {
        private readonly IQuizService _quizService;

        public GenerateQuizCommandHandler(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public async Task<GenerateQuizResponse> Handle(GenerateQuizCommand command, CancellationToken cancellationToken)
        {
            var quizDto = await _quizService.GenerateQuizFromPromptTemplateAsync(command);

            return quizDto;
        }
    }
}
