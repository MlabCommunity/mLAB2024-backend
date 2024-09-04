using MediatR;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;

public record UpdateQuestionAndAnswersCommand(
    Guid Id,
    string Title,
    List<UpdateAnswer> UpdateAnswers) : ICommand<Unit>;
public record UpdateAnswer(
    Guid Id,
    string Content,
    bool IsCorrect);