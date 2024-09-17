using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Dtos.Profile;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Infrastructure.Services.Identity;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProfileService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserProfileDto> GetProfileAsync()
    {
        var id = _httpContextAccessor.GetUserId();
        var currentUser = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException(nameof(User), id);

        var userProfileDto = new UserProfileDto(currentUser.Id, currentUser.Email!, currentUser.DisplayName!);
        return userProfileDto;
    }

    public async Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest request)
    {
        var id = _httpContextAccessor.GetUserId();
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new NotFoundException(nameof(User), id);

        user.DisplayName = request.DisplayName;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            HandleIdentityErrors(result.Errors, "Failed to update user");
        }

        return new UserProfileDto(user.Id, user.Email!, user.DisplayName!);
    }

    public async Task ConvertGuestToUser(RegisterRequestDto request)
    {
        var userId = _httpContextAccessor.GetUserId();

        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);

        if (!user.IsGuest)
        {
            throw new BadRequestException("Only guest accounts can be converted to regular accounts.");
        }

        user.Email = request.Email;
        user.IsGuest = false;

        await UpdateUser(user, request.Password);
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

    private void HandleIdentityErrors(IEnumerable<IdentityError> errors, string message)
    {
        var errorDictionary = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description).ToArray()
            );

        throw new BadRequestException(message, errorDictionary);
    }
}