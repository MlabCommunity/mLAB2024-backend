using QuizBackend.Domain.Common;

namespace QuizBackend.Domain.Entities;
public class UserAnswer : BaseEntity
{
    public Guid QuizParticipationId { get; set; }
    public QuizParticipation QuizParticipation { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
}