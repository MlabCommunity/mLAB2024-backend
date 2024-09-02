using QuizBackend.Domain.Common;

namespace QuizBackend.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public required string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}