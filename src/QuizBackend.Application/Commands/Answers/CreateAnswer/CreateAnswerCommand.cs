using QuizBackend.Application.Interfaces.Messaging;


namespace QuizBackend.Application.Commands.Answers.CreateAnswer
{
    public record CreateAnswerCommand(string Content, bool IsCorrect, Guid QuestionId) : ICommand<Guid>;
}
