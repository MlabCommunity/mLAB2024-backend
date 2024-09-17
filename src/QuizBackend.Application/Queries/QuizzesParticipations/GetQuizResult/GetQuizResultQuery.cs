using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;

public record GetQuizResultQuery(Guid QuizParticipationId) : IQuery<QuizResultResponse>;