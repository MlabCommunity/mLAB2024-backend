using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Availability Availability { get; set; } = Availability.Public;
        public required string OwnerId { get; init; }
        public User Owner { get; set; } = null!;
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? UpdatedAtUtc { get; set; }
        public List<Question> Questions { get; set; } = [];
        public List<User> Participants { get; set; } = [];
    }
}
