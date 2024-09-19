using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Infrastructure.Authorization;
using QuizBackend.Infrastructure.Authorization.Requirements;
using System.Security.Claims;

namespace QuizBackend.Infrastructure.Extensions;

public static class AuthorizationPolicies
{
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.User, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "User");
            })
            .AddPolicy(PolicyNames.Guest, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "Guest");
            })
            .AddPolicy(PolicyNames.QuizOwner, policy =>
            {
                policy.Requirements.Add(new QuizOwnerRequirement());
            })
            .AddPolicy(PolicyNames.QuestionOwner, policy =>
            {
                policy.Requirements.Add(new QuestionOwnerRequirement());
            });

        services.AddScoped<IAuthorizationHandler, QuizOwnerAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, QuestionOwnerAuthorizationHandler>();
    }
}
