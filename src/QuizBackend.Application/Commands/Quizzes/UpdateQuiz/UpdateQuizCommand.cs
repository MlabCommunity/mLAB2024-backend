using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.UpdateQuiz
{
    public record UpdateQuizCommand(
        Guid Id,
        string Title,
        string Description
        ) : ICommand<Guid>;
}