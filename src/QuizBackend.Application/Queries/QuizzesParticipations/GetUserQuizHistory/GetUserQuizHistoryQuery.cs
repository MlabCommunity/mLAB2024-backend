using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.QuizzesParticipations.GetUserAnswer;

public record GetUserQuizHistoryQuery() : IQuery<List<QuizParticipationHistoryResponse>>;