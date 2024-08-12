using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto, HttpResponse response);
        Task<LogoutResponseDto> LogoutAsync(HttpContext httpContext);
        Task<SignUpResponseDto> SignUp(RegisterRequestDto request);
        Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId, HttpResponse response);
    }
}