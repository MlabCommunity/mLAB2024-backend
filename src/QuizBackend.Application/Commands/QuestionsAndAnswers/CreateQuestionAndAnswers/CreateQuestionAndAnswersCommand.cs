using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;

public record CreateQuestionAndAnswersCommand(
    string Title,
    List<CreateAnswer> CreateAnswers,
    Guid QuizId) : ICommand<Guid>;
public record CreateAnswer(
    string Content,
    bool IsCorrect
    );