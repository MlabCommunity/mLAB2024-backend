using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.UpdateQuiz
{
    public record UpdateQuizCommand(UpdateQuizDto quizDto) : ICommand<Guid>;
}
