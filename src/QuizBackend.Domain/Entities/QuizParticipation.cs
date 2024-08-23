namespace QuizBackend.Domain.Entities
{
    public class QuizParticipation
    { 
        public Guid Id { get; set; } 
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public string ParticipantId { get; set; } = null!;
        public User Participant { get; set; } = null!;
        public DateTime ParticipationDateUtc { get; set; }
    }
}