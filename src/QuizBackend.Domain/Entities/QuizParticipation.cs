namespace QuizBackend.Domain.Entities
{
    public class QuizParticipation
    { 
        public Guid Id { get; private init; } = Guid.NewGuid();
        public required Guid QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public required string ParticipantId { get; set; }
        public User Participant { get; set; } = null!;
        public DateTime ParticipationDateUtc { get; set; }
    }
}