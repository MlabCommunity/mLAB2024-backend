using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Exceptions;
using System.Security.Claims;


namespace QuizBackend.Infrastructure.Services.Identity
{
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
                    throw new BadRequestException("User context is not present");
                }

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedException("User is not authenticated");
                }

                var id = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
                return id;
            }
        }
    }
}
