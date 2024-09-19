using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IRoleService _roleService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IRoleService roleService, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _roleService = roleService;
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

        await _signInManager.SignInAsync(user, isPersistent: false);

        return await GenerateJwtAuthResultAsync(user);
    }

    public async Task<LogoutResponseDto> LogoutAsync()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (userId != null)
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
        var displayName = ExtractDisplayNameFromEmail(request.Email);
        var userName = GenerateUniqueUserName();
        var user = new User
        {
            UserName = userName,
            Email = request.Email,
            DisplayName = displayName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(e => new ValidationFailure() { PropertyName = e.Code, ErrorMessage = e.Description })
                .ToList();

            throw new ValidationException("User creation failed", errors);
        }

        await _roleService.AssignRole(user, AppRole.User);

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

    public async Task<JwtAuthResultDto> CreateAndAuthenticateGuest(string displayName)
    {
        var guest = new User
        {
            Email = $"guest{Guid.NewGuid().ToString("N").Substring(0, 8)}@guest.com",
            UserName = $"guest{Guid.NewGuid().ToString("N").Substring(0, 8)}",
            DisplayName = displayName.Trim(),
        };

        var result = await _userManager.CreateAsync(guest);
       
        if (!result.Succeeded)
        {
            throw new BadRequestException($"Unable to create guest user: {string.Join(", "
                ,result.Errors.Select(e => e.Description))}");
        }

        await _roleService.AssignRole(guest, AppRole.Guest);

        var tokens = await GenerateJwtAuthResultAsync(guest);
        return new JwtAuthResultDto
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };
    }

    private async Task<JwtAuthResultDto> GenerateJwtAuthResultAsync(User user)
    {
        var claims = await _jwtService.GetClaimsAsync(user);
        var accessToken = _jwtService.GenerateJwtToken(claims);
        var refreshToken = await _jwtService.GenerateOrRetrieveRefreshTokenAsync(user.Id);

        return new JwtAuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string ExtractDisplayNameFromEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email.Substring(0, atIndex) : email;
    }
    private string GenerateUniqueUserName()
    {
        return $"user-{Guid.NewGuid().ToString().Substring(0, 10)}";
    }
}