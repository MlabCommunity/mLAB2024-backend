using QuizBackend.Application.Dtos;
using QuizBackend.Domain.Entities;
using System.Security.Claims;

namespace QuizBackend.Application.Interfaces.Users
{
    public interface IJwtService
    {
        string GenerateJwtToken(List<Claim> claims);
        Task<string> GenerateRefreshTokenAsync(string userId);
        Task<List<Claim>> GetClaimsAsync(User user);
        Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId);
        Task InvalidateRefreshTokenAsync(string userId);
    }
}
