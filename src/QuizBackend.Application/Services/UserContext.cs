using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizBackend.Application.Extensions;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Services
{
    public interface IUserContext
    {
        public string UserId { get; } 

    }
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
     
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return id ?? throw new AuthenticationException("User context is unavailable or user is not authenticated");
            }
        }
       
    }
}
