using MediatR;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;

public record SubmitQuizAnswerCommand(
    Guid QuizParticipationId,
    Guid QuestionId,
    Guid AnswerId) : ICommand<Unit>;