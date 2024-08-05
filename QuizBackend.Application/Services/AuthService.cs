using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Dtos;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Services
{
    public interface IAuthService
    {
        Task<(bool succeed, Guid UserId)> SignUp(RegisterRequestDto request);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<(bool succeed, Guid UserId)> SignUp(RegisterRequestDto request)
        {
          
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return (false, Guid.Empty); 
            }

            var user = new User { UserName = request.Email, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
               
                return (false, Guid.Empty);
            }

            return (true, user.Id); 
        }

    }
}
