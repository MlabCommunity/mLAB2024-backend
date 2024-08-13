using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var id = _userContext.UserId;
            var currentUser = await _userManager.FindByIdAsync(id)
                ?? throw new NotFoundException(nameof(User), id);

            var userProfileDto = new UserProfileDto
            {
                Id = currentUser.Id,
                Email = currentUser.Email ?? string.Empty,
                UserName = currentUser.UserName ?? string.Empty,
            };

            return userProfileDto;
        }

        public async Task<UserProfileDto> UpdateProfileAsync(UpdateUserProfileRequest request)
        {
            var id = _userContext.UserId;
            var user = await _userManager.FindByIdAsync(id) 
                ?? throw new NotFoundException(nameof(User), id);

            user.UserName = request.UserName;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new BadRequestException($"Error updating user profile:", errors);
            }

           return new UserProfileDto 
           { 
               Id = user.Id, 
               Email = user.Email!, 
               UserName = user.UserName 
           };
        }
    }
}
