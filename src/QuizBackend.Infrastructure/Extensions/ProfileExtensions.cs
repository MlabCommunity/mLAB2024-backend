using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.ExceptionsHandlers;
using QuizBackend.Infrastructure.Services.Identity;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class ProfileExtensions
    {
        public static void AddProfileExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddHttpContextAccessor();
        }
    }
}
