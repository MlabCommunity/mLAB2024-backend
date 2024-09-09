using QuizBackend.Domain.Common;

namespace QuizBackend.Domain.Entities;
public class UserAnswer : BaseEntity
{
    public Guid QuizParticipationId { get; set; }
    public QuizParticipation QuizParticipation { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public Guid AnswerId { get; set; }
    public Answer Answer { get; set; } = null!;
}