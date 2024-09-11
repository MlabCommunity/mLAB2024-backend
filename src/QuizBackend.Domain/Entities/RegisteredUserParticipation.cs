namespace QuizBackend.Domain.Entities;
public class RegisteredUserParticipation : QuizParticipation
{
    public string ParticipantId { get; set; } = null!;
    public User Participant { get; set; } = null!;
}