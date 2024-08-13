using QuizBackend.Application.Dtos.Profile;

namespace QuizBackend.Application.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfileDto> GetProfileAsync();
        Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest request);
    }
}
