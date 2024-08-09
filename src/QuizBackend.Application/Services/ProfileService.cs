using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserContext _userContext;

        public ProfileService(UserManager<User> userManager, IUserContext userContext) 
        {
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<UserProfileDto> GetProfileAsync()
        {
            var currentUser = await _userManager.FindByIdAsync(_userContext.UserId);

            if (currentUser == null) throw new ApplicationException("User not found");

            var userProfileDto = new UserProfileDto
            {
                Id = currentUser.Id,
                Email = currentUser.Email ?? string.Empty,
                UserName = currentUser.UserName ?? string.Empty,
            };

            return userProfileDto;
        }

    }
}
