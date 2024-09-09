namespace QuizBackend.Domain.Entities;
public class GuestUserParticipation : QuizParticipation
{
    public string GuestParticipant { get; set; } = null!;
}