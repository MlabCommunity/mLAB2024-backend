using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using System.Security.Claims;

namespace QuizBackend.Infrastructure.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JwtAuthResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new NotFoundException(nameof(user), loginDto.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new BadRequestException("Email or Password is incorrect");

            var claims = await _jwtService.GetClaimsAsync(user);
            await _signInManager.SignInAsync(user, isPersistent: false);

            var accessToken = _jwtService.GenerateJwtToken(claims);
            var refreshToken = await _jwtService.GenerateOrRetrieveRefreshTokenAsync(user.Id);

            return new JwtAuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<LogoutResponseDto> LogoutAsync()
        {
            var userId = _httpContextAccessor.GetUserId();

            if(userId != null)
            {
                await _jwtService.InvalidateRefreshTokenAsync(userId);
            }

            await _signInManager.SignOutAsync();

            return new LogoutResponseDto
            {
                Message = "User has been logged out successfully."
            };
        }

        public async Task<SignUpResponseDto> SignUpAsync(RegisterRequestDto request)
        {
            var user = new User { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(e => new ValidationFailure() { PropertyName = e.Code, ErrorMessage = e.Description })
                    .ToList();

                throw new ValidationException("User creation failed", errors);
            }

            return new SignUpResponseDto
            {
                Message = "Registration successful. Please sign in now",
            };
        }

        public async Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken)
        {
            var jwtAuthResult = await _jwtService.RefreshTokenAsync(refreshToken);
            return jwtAuthResult;
        }
    }
}
