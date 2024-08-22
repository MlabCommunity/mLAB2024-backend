namespace QuizBackend.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public required int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}