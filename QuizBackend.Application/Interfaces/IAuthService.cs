using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task<(bool succeed, string UserId)> SignUp(RegisterRequestDto request);
    }
}