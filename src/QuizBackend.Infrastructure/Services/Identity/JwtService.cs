﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuizBackend.Infrastructure.Services.Identity;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtService(IConfiguration configuration, AppDbContext appDbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider)
    {
        _configuration = configuration;
        _appDbContext = appDbContext;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateJwtToken(List<Claim> claims)
    {
        var secretKey = _configuration["Jwt:SecretKey"];
        var expire = _configuration.GetValue<double>("Jwt:AccessTokenExpirationMinutes");

        var key = secretKey != null
            ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            : throw new InvalidConfigurationException("JWT secret key is not configured or is empty.");

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(expire),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<List<Claim>> GetClaimsAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name,  user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    public async Task<string> GenerateOrRetrieveRefreshTokenAsync(string userId)
    {
        var existingToken = await _appDbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Expires > _dateTimeProvider.UtcNow && !rt.IsRevoked);

        if (existingToken != null)
        {
            return existingToken.Token;
        }

        return await GenerateRefreshTokenAsync(userId);
    }

    public async Task<string> GenerateRefreshTokenAsync(string userId)
    {
        var expire = _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays");

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = GenerateRandomToken(),
            UserId = userId,
            Expires = _dateTimeProvider.UtcNow.AddDays(expire),
            Created = _dateTimeProvider.UtcNow
        };

        _appDbContext.RefreshTokens.Add(refreshToken);
        await _appDbContext.SaveChangesAsync();

        return refreshToken.Token;
    }

    public async Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken)
    {
        var refreshTokenEntity = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (refreshTokenEntity == null)
        {
            throw new BadRequestException("Invalid refresh token");
        }
        else if(refreshTokenEntity.Expires <= _dateTimeProvider.UtcNow || refreshTokenEntity.IsRevoked)
        {
            throw new UnauthorizedAccessException("Refresh token has expired or has been revoked");
            //TODO in future make UnauthorizedException in Domain Exceptions
        }

        var userId = refreshTokenEntity.UserId;
        _appDbContext.RefreshTokens.Update(refreshTokenEntity);
        await _appDbContext.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException(nameof(user), userId);
        }

        var claims = await GetClaimsAsync(user);
        var newJwtToken = GenerateJwtToken(claims);

        return new JwtAuthResultDto
        {
            AccessToken = newJwtToken,
            RefreshToken = refreshToken
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
            token.Revoked = _dateTimeProvider.UtcNow;
        }

        _appDbContext.RefreshTokens.UpdateRange(refreshTokens);
        await _appDbContext.SaveChangesAsync();
    }

    public string GetAccessTokenFromCookie()
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"]!;
    }
}