﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Services;
using System.Security.Authentication;



namespace QuizBackend.Api.Controllers
{
    [ApiController]
    [Route("api/profile")]
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
            var profile = await _profileService.GetProfileAsync();
            return Ok(profile);
        }
    }
}
