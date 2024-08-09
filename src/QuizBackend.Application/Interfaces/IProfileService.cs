using QuizBackend.Application.Dtos.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Interfaces
{
    public interface IProfileService
    {
        Task<UserProfileDto> GetProfileAsync();
        Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest profile);
    }
}
