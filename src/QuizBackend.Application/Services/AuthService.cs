using FluentValidation;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
        private readonly IValidator<RegisterRequestDto> _registerRequestValidator;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IValidator<RegisterRequestDto> registerRequestValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _registerRequestValidator = registerRequestValidator;
        }

        public async Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new InvalidCredentialException("Invalid login attempt");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new InvalidCredentialException("Invalid login attempt");

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

        public async Task<SignUpResponseDto> SignUpAsync(RegisterRequestDto request)
        {
            var validationResult = await _registerRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors;
                throw new ValidationException("Validation failed", errorMessages);
            }

            var user = new User { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToArray();
                throw new ValidationException($"User creation failed: {string.Join(", ", errorMessages)}");
            }

            return new SignUpResponseDto
            {
                Succeed = true,
                UserId = user.Id,
                Errors = []
            };
        }

    }
}
