using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Dtos.Profile;

namespace QuizBackend.Application.Interfaces.Users;

public interface IProfileService
{
    Task<JwtAuthResultDto> ConvertGuestToUser(RegisterRequestDto request);
    Task<UserProfileDto> GetProfileAsync();
    Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest request);
    Task DeleteProfile();
}