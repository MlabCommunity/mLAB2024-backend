using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync(HttpContext httpContext);
        Task<SignUpResponseDto> SignUp(RegisterRequestDto request);
    }
}