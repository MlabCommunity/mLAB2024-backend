using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using System.Security.Authentication;
using System.Security.Claims;

namespace QuizBackend.Infrastructure.Services
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
                throw new InvalidCredentialException("Invalid login attempt");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                throw new InvalidCredentialException("Invalid login attempt");

            var claims = await _jwtService.GetClaimsAsync(user);
            await _signInManager.SignInAsync(user, isPersistent: false);

            var accessToken = _jwtService.GenerateJwtToken(claims);
            var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user.Id);

            _jwtService.SetAccessTokenCookie(accessToken);
            _jwtService.SetRefreshTokenCookie(refreshToken);

            return new JwtAuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<LogoutResponseDto> LogoutAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId != null)
            {
                await _jwtService.InvalidateRefreshTokenAsync(userId);
            }

            await _signInManager.SignOutAsync();

            _jwtService.SetAccessTokenCookie("");
            _jwtService.SetRefreshTokenCookie("");

            return new LogoutResponseDto
            {
                Message = "User has been logged out successfully."
            };
        }

        public async Task<SignUpResponseDto> SignUp(RegisterRequestDto request)
        {

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new SignUpResponseDto
                {
                    Succeed = false,
                    UserId = string.Empty,
                    Message = "User is already exist"
                };
            }

            var user = new User { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {

                return new SignUpResponseDto
                {
                    Succeed = false,
                    UserId = string.Empty,
                    Message = "Error in sign up"
                };
            }

            return new SignUpResponseDto
            {
                Succeed = true,
                UserId = user.Id,
                Message = "User has been created successfully"
            };
        }

        public async Task<JwtAuthResultDto> RefreshTokenAsync(string refreshToken, string userId)
        {
            var jwtAuthResult = await _jwtService.RefreshTokenAsync(refreshToken, userId);
            _jwtService.SetAccessTokenCookie(jwtAuthResult.AccessToken);
            _jwtService.SetRefreshTokenCookie(jwtAuthResult.RefreshToken);

            return jwtAuthResult;
        }
    }
}
