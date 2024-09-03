using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Profile;
using QuizBackend.Application.Interfaces.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves the current user's profile.", Description = "Returns the profile of the currently authenticated user.")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var profile = await _profileService.GetProfileAsync();
            return Ok(profile);
        }

        [Authorize]
        [HttpPut]
        [SwaggerOperation(Summary = "Updates the current user's profile.", Description = "Updates the profile of the currently authenticated user with the provided data.")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var updatedUser = await _profileService.UpdateProfileAsync(request);
            return Ok(updatedUser);
        }

    }
}