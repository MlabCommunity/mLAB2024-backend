namespace QuizBackend.Application.Dtos.Profile

{
    public class UserProfileDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; } 
        public required string UserName { get; set; }
    }
}
