using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("Invalid login attempt");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid login attempt");

            var claims = await _jwtService.GetClaimsAsync(user);
            await _signInManager.SignInAsync(user, isPersistent: false);

            var accessToken = _jwtService.GenerateJwtToken(claims);
            var refreshToken = string.Empty;

            return new JwtAuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<(bool succeed, string UserId)> SignUp(RegisterRequestDto request)
        {

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return (false, string.Empty);
            }

            var user = new User { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {

                return (false, string.Empty);
            }

            return (true, user.Id);
        }

    }
}
