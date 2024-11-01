﻿using QuizBackend.Domain.Common;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities;

public class QuizParticipation : BaseEntity
{
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public string ParticipantId { get; set; } = null!;
    public User Participant { get; set; } = null!;
    public DateTime ParticipationDateUtc { get; set; }
    public QuizParticipationStatus Status { get; set; } = QuizParticipationStatus.Started;
    public QuizResult? QuizResult { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
    public DateTime? CompletionTime { get; set; }
}