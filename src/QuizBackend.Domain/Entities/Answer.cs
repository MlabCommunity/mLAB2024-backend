namespace QuizBackend.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public required Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}