using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? UpdatedAtUtc { get; set; }
        public ICollection<Answer> Answers { get; set; } = [];
        public QuestionType QuestionType { get; set; }
        public required Guid QuizId {  get; set; }
        public Quiz Quiz { get; set; } = null!;
    }
}