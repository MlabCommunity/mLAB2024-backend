using Microsoft.AspNetCore.Http;
using QuizBackend.Domain.Exceptions;
using System.Security.Claims;

namespace QuizBackend.Application.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
            throw new BadRequestException("User not present");

        return user.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
