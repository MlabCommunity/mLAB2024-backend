using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Extensions;

public static class SeedExtensions
{
    public static async Task Seed(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        string[] roles = ["User", "Guest"];

        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role)) continue;
            await roleManager.CreateAsync(new Role(role));
        }
    }
}