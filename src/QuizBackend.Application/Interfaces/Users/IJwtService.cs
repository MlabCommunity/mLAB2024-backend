using QuizBackend.Application.Dtos;
using QuizBackend.Domain.Entities;
using System.Security.Claims;

namespace QuizBackend.Application.Interfaces.Users
{
    public interface IJwtService
    {
        string GenerateJwtToken(List<Claim> claims);
        Task<string> GenerateRefreshTokenAsync(string userId);
        string GetAccessTokenFromCookie();
        Task<List<Claim>> GetClaimsAsync(User user);
        Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken);
        void SetAccessTokenCookie(string token);
        void SetRefreshTokenCookie(string refreshToken);
        Task InvalidateRefreshTokenAsync(string userId);
        Task<string> GenerateOrRetrieveRefreshTokenAsync(string userId);
    }
}