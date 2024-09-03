namespace QuizBackend.Application.Dtos.Auth
{
    public class RegisterRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
