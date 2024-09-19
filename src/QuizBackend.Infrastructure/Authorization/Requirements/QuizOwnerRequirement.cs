using Microsoft.AspNetCore.Authorization;

namespace QuizBackend.Infrastructure.Authorization.Requirements;

public class QuizOwnerRequirement : IAuthorizationRequirement
{
    public QuizOwnerRequirement(){}
}