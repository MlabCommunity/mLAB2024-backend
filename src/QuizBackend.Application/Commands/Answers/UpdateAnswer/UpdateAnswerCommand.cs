using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Answers.UpdateAnswer
{
    public record UpdateAnswerCommand(Guid Id, string Content, bool IsCorrect, Guid QuestionId) : ICommand<Guid>;
}
