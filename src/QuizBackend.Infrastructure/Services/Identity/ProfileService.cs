using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Dtos.Profile;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.Identity;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly IRoleService _roleService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtService _jwtService;
    private readonly SignInManager<User> _signInManager;
    private readonly IQuizRepository _quizRepository;

    public ProfileService(UserManager<User> userManager, IRoleService roleService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService, SignInManager<User> signInManager, IQuizRepository quizRepository)
    {
        _userManager = userManager;
        _roleService = roleService;
        _httpContextAccessor = httpContextAccessor;
        _jwtService = jwtService;
        _signInManager = signInManager;
        _quizRepository = quizRepository;
    }

    public async Task<UserProfileDto> GetProfileAsync()
    {
        var id = _httpContextAccessor.GetUserId();
        var currentUser = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException(nameof(User), id);

        var userProfileDto = new UserProfileDto(currentUser.Id, currentUser.Email!, currentUser.DisplayName!, currentUser.ImageUrl!);
        return userProfileDto;
    }

    public async Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest request)
    {
        var id = _httpContextAccessor.GetUserId();
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException(nameof(User), id);

        user.DisplayName = request.DisplayName;
        if (!string.IsNullOrEmpty(request.ImageUrl) && !IsValidUrl(request.ImageUrl))
        {
            throw new BadRequestException("Invalid URL format for ImageUrl");
        }

        user.ImageUrl = request.ImageUrl;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            HandleIdentityErrors(result.Errors, "Failed to update user");
        }

        return new UserProfileDto(user.Id, user.Email!, user.DisplayName!, user.ImageUrl!);
    }

    public async Task<JwtAuthResultDto> ConvertGuestToUser(RegisterRequestDto request)
    {
        var userId = _httpContextAccessor.GetUserId();

        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);

        user.Email = request.Email;
    
        await UpdateUser(user, request.Password);
        await _roleService.RemoveRole(user, AppRole.Guest);
        await _roleService.AssignRole(user, AppRole.User);

        await _jwtService.InvalidateRefreshTokenAsync(userId);
        await _signInManager.SignOutAsync();

        var claims = await _jwtService.GetClaimsAsync(user);
        var accessToken = _jwtService.GenerateJwtToken(claims);

        var refreshToken = await _jwtService.GenerateOrRetrieveRefreshTokenAsync(user.Id);

        return new JwtAuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
    private async Task UpdateUser(User user, string newPassword)
    {
        var errors = new List<IdentityError>();

        var addPasswordResult = await _userManager.AddPasswordAsync(user, newPassword);
        if (!addPasswordResult.Succeeded)
        {
            errors.AddRange(addPasswordResult.Errors);
        }

        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded)
        {
            errors.AddRange(updateUserResult.Errors);
        }

        if (errors.Count != 0)
        {
            HandleIdentityErrors(errors, "Failed to update user email or password.");
        }
    }

    public async Task DeleteProfile()
    {
        var userId = _httpContextAccessor.GetUserId();
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);

        user.IsDeleted = true;
        user.Email = "DELETED-USER";
        user.NormalizedEmail = "DELETED-USER";
        user.DisplayName = "DELETED USER";
        await _quizRepository.UpdateQuizzesStatusForUser(userId, Status.Inactive);
    }

    private void HandleIdentityErrors(IEnumerable<IdentityError> errors, string message)
    {
        var errorDictionary = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).Distinct().ToArray()
            );

        throw new BadRequestException(message, errorDictionary);
    }

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}