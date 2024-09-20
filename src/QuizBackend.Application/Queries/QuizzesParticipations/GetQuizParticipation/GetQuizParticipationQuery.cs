using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizParticipation;

public record GetQuizParticipationQuery(Guid Id) : IQuery<QuizParticipationResponse>;