using Microsoft.AspNetCore.Identity;

namespace QuizBackend.Domain.Entities;

public class Role : IdentityRole
{
    private Role() : base() { }
    public Role(string roleName) : base(roleName) { }
}