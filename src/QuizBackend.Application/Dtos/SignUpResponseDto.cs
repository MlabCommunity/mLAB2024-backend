
namespace QuizBackend.Application.Dtos
{
    public class SignUpResponseDto
    {
        public bool Succeed { get; set; }
        public required string UserId { get; set; }
        public required string Message { get; set; }
    }
}
