using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Interfaces.Users;

public interface IAuthService
{
    Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto);
    Task<LogoutResponseDto> LogoutAsync();
    Task<SignUpResponseDto> SignUpAsync(RegisterRequestDto request);
    Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken);
    Task<JwtAuthResultDto> LoginGuest(User guestUser);
    Task<User> CreateGuestUser(string displayName);
}