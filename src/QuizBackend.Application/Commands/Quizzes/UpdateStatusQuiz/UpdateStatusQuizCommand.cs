using QuizBackend.Application.Commands.Quizzes.UpdateStatusQuiz;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.UpdateStatusQuiz
{
    public record UpdateStatusQuizCommand(Guid Id, Status Status) : ICommand<UpdateQuizStatusResponse>;
}