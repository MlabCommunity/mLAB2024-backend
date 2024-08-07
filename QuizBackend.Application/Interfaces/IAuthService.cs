using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}