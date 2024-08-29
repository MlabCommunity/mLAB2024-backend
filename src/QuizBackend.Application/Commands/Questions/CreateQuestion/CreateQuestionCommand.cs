using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.CreateQuestion
{
    public record CreateQuestionCommand(string Title, Guid QuizId, List<CreateAnswer> CreateAnswers) : ICommand<Guid>;
    public record CreateAnswer(string Content, bool IsCorrect, Guid QuestionId);
}
