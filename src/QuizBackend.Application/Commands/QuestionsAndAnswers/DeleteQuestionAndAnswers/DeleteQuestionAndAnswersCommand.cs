using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.DeleteQuestionAndAnswers
{
    public record DeleteQuestionAndAnswersCommand(Guid Id) : ICommand<Guid>;
}
