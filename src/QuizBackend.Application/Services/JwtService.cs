using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using QuizBackend.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuizBackend.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration, AppDbContext appDbContext, UserManager<User> userManager)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = GenerateRandomToken(),
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            _appDbContext.RefreshTokens.Add(refreshToken);
            await _appDbContext.SaveChangesAsync();

            return refreshToken.Token;
        }

        public async Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId)
        {
            var refreshTokenEntity = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (refreshTokenEntity == null || refreshTokenEntity.Expires <= DateTime.UtcNow || refreshTokenEntity.IsRevoked)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            refreshTokenEntity.IsRevoked = true;
            refreshTokenEntity.Revoked = DateTime.UtcNow;
            _appDbContext.RefreshTokens.Update(refreshTokenEntity);
            await _appDbContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new SecurityTokenException("User not found");
            }

            var claims = await GetClaimsAsync(user);
            var newJwtToken = GenerateJwtToken(claims);
            var newRefreshToken = await GenerateRefreshTokenAsync(userId);

            return new JwtAuthResultDto
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }
        private string GenerateRandomToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task InvalidateRefreshTokenAsync(string userId)
        {
            var refreshTokens = await _appDbContext.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync();

            foreach (var token in refreshTokens)
            {
                token.IsRevoked = true;
                token.Revoked = DateTime.UtcNow;
            }

            _appDbContext.RefreshTokens.UpdateRange(refreshTokens);
            await _appDbContext.SaveChangesAsync();
        }

        private CookieOptions CreateCookieOptions()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
        }

        public void SetAccessTokenCookie(string token, HttpResponse response)
        {
            response.Cookies.Append("AccessToken", token, CreateCookieOptions());
        }

        public void SetRefreshTokenCookie(string refreshToken, HttpResponse response)
        {
            response.Cookies.Append("RefreshToken", refreshToken, CreateCookieOptions());
        }

        public string GetAccessTokenFromCookie(HttpRequest request)
        {
            return request.Cookies["AccessToken"]!;
        }
    }
}
