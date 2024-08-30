using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public record CreateQuizCommand(CreateQuizDto QuizDto) : ICommand<Guid>;
}