using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.UpdateQuiz
{
    public record UpdateQuizCommand(
        string Title,
        string Description
        ) : ICommand<Guid>;
}
