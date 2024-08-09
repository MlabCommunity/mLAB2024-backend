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
                var user = _httpContextAccessor?.HttpContext?.User;
                if (user == null)
                {
                    throw new InvalidOperationException("User context is not present");
                }

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }

                var id = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return id;
            }
        }
       
    }
}
