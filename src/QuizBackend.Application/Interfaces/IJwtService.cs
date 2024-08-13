using QuizBackend.Application.Dtos;
using QuizBackend.Domain.Entities;
using System.Security.Claims;

namespace QuizBackend.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(List<Claim> claims);
        Task<string> GenerateRefreshTokenAsync(string userId);
        string GetAccessTokenFromCookie();
        Task<List<Claim>> GetClaimsAsync(User user);
        Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId);
        void SetAccessTokenCookie(string token);
        void SetRefreshTokenCookie(string refreshToken);
        Task InvalidateRefreshTokenAsync(string userId);
    }
}