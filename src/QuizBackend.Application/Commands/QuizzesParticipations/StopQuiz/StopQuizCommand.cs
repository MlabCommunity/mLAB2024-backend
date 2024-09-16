using MediatR;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.QuizzesParticipations.StopQuiz;

public record StopQuizCommand(Guid QuizParticipationId) : ICommand<Unit>;