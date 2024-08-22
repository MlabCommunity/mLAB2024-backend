using Microsoft.AspNetCore.Identity;

namespace QuizBackend.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<Quiz> OwnedQuizzes { get; set; } = [];
        public List<Quiz> ParticipatedQuizzes { get; set; } = [];
    }
}
