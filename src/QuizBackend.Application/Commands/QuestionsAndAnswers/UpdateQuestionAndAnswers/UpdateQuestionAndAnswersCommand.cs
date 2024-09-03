using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers
{
    public record UpdateQuestionAndAnswersCommand(
        Guid Id,
        string Title,
        List<UpdateQuestionAnswer> UpdateQuestionAnswers) : ICommand<Guid>;

    public record UpdateQuestionAnswer(
        Guid Id,
        string Content,
        bool IsCorrect);
}
