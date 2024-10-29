using QuizBackend.Domain.Common;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities;

public class Question : BaseEntity
{
    public required string Title { get; set; }
    public ICollection<Answer> Answers { get; set; } = [];
    public QuestionType QuestionType { get; set; }
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
}