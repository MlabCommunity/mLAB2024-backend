using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Infrastructure.Authorization;
using QuizBackend.Infrastructure.Interfaces;
using QuizBackend.Infrastructure.Services.Identity;
using System.Security.Claims;

namespace QuizBackend.Infrastructure.Extensions;

public static class AuthExtension
{
    public static void AddAuthExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.User, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "User");
            })
            .AddPolicy(PolicyNames.Guest, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "Guest");
            });
    }
}