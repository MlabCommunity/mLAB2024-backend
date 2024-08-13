using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Services;
using QuizBackend.Infrastructure.Services.Identity;

namespace QuizBackend.Application.Extensions
{
    public static class AuthExtension
    {
        public static void AddAuthExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
