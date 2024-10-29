using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Dtos.Quizzes;

public record ParticipantDto(string DisplayName, double? Score, QuizParticipationStatus Status, DateTime ParticipationDateUtc);