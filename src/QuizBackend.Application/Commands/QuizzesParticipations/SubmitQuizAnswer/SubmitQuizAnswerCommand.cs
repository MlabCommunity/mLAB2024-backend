using MediatR;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;

public record SubmitQuizAnswerCommand(
    Guid QuizParticipationId,
    List<Guid> QuestionsId,
    List<Guid> AnswersId) : ICommand<Unit>;