using Microsoft.AspNetCore.Identity;

namespace QuizBackend.Domain.Entities;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Quiz> OwnedQuizzes { get; set; } = [];
    public ICollection<Quiz> ParticipatedQuizzes { get; set; } = [];
    public bool IsDeleted { get; set; }
}