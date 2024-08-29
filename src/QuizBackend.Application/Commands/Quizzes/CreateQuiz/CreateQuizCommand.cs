using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public record CreateQuizCommand(CreateQuizDto QuizDto) : ICommand<Guid>;
    //TODO change arguments to eliminate using CreateQuizDto after demo release
}