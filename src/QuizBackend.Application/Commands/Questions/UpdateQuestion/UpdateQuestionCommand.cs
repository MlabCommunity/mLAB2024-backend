using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.UpdateQuestion
{
    public record UpdateQuestionCommand(Guid Id, string Title, List<AnswerDto> Answers) : ICommand<Guid>;
}
