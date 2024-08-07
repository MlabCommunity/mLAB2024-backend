using QuizBackend.Application.Dtos;
using QuizBackend.Domain.Entities;
using System.Security.Claims;

namespace QuizBackend.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(List<Claim> claims);
        Task<List<Claim>> GetClaimsAsync(User user);
    }
}