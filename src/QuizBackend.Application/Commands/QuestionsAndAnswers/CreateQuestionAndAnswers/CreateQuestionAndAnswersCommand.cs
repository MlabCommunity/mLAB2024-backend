using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers
{
    public record CreateQuestionAndAnswersCommand(
        string Title,
        List<CreateQuestionAnswer> CreateQuestionAnswers,
        Guid QuizId) : ICommand<Guid>;
    public record CreateQuestionAnswer(
        string Content,
        bool IsCorrect
        );
}