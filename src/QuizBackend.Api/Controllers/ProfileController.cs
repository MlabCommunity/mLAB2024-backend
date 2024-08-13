using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Profile;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Services;
using QuizBackend.Domain.Entities;
using System.Security.Authentication;



namespace QuizBackend.Api.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var profile = await _profileService.GetProfileAsync();
            return Ok(profile);
        }

        [HttpPut]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var updatedUser = await _profileService.UpdateProfileAsync(request);
            return Ok(updatedUser);
        }
        [HttpPut]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            var updatedUser = await _profileService.UpdateProfileAsync(request);
            return Ok(updatedUser);
        }
    }
}
