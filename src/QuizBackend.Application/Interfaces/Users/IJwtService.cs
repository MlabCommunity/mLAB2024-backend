using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Domain.Entities;
using System.Security.Claims;

namespace QuizBackend.Application.Interfaces.Users;

public interface IJwtService
{
    string GenerateJwtToken(List<Claim> claims);
    Task<List<Claim>> GetClaimsAsync(User user);
    Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken);
    Task InvalidateRefreshTokenAsync(string userId);
    Task<string> GenerateOrRetrieveRefreshTokenAsync(string userId);
    Task<List<Claim>> GetClaimsForGuest(User user);
    Task<string> GenerateRefreshTokenAsync(string userId);
}