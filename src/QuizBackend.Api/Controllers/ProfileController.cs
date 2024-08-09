using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Profile;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Services;
using QuizBackend.Domain.Entities;
using System.Security.Authentication;



namespace QuizBackend.Api.Controllers
{
    [ApiController]
    [Route("api/profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserContext _userContext;

        public ProfileController(IProfileService profileService, IUserContext userContext)
        {
            _profileService = profileService;
            _userContext = userContext;
        }

        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            try
            {
                var profile = await _profileService.GetProfileAsync();
                return Ok(profile);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var updatedUser = await _profileService.UpdateProfileAsync(request);
            return Ok(updatedUser);
        }
    }
}
