using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Answers.UpdateAnswer
{
    public record UpdateAnswerCommand(UpdateAnswerDto AnswerDto) : ICommand<Guid>;
}
