﻿using QuizBackend.Domain.Enums;

namespace QuizBackend.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Active;
        public Availability Availability { get; set; } = Availability.Public;
        public string OwnerId { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? UpdatedAtUtc { get; set; }
        public ICollection<Question> Questions { get; set; } = [];
        public ICollection<User> Participants { get; set; } = [];
    }
}