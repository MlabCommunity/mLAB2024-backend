using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Availability Availability { get; set; } = Availability.Public;
        public required string OwnerId { get; init; }
        public User Owner { get; set; } = null!;
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? UpdatedAtUtc { get; set; }
        public ICollection<Question> Questions { get; set; } = [];
        public ICollection<User> Participants { get; set; } = [];
    }
}
