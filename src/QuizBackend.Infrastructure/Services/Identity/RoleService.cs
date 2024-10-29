using Microsoft.AspNetCore.Identity;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.Identity;

public class RoleService : IRoleService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public RoleService(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task AssignRole(User user, AppRole role)
    {
        var roleName = role.ToString();
        var roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExists)
        {
            throw new InvalidOperationException($"Role '{roleName}' does not exist.");
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }

    public async Task RemoveRole(User user, AppRole role)
    {
        var roleName = role.ToString();
        if (await _userManager.IsInRoleAsync(user, roleName))
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}