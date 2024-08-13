
namespace QuizBackend.Application.Dtos
{
    public class JwtAuthResultDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
