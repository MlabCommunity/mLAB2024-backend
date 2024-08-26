﻿using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Interfaces.Users
{
    public interface IAuthService
    {
        Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto);
        Task<LogoutResponseDto> LogoutAsync();
        Task<SignUpResponseDto> SignUpAsync(RegisterRequestDto request);
        Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId);
    }
}