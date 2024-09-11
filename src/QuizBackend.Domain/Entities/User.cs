using Microsoft.AspNetCore.Identity;

namespace QuizBackend.Domain.Entities;

public class User : IdentityUser
{
    public bool IsGuest { get; set; }
    public string? DisplayName { get; set; }
    public ICollection<Quiz> OwnedQuizzes { get; set; } = [];
    public ICollection<Quiz> ParticipatedQuizzes { get; set; } = [];
}