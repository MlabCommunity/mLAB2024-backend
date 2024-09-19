using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Infrastructure.Authorization.Requirements;

public class QuizOwnerAuthorizationHandler : AuthorizationHandler<QuizOwnerRequirement, Guid>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuizOwnerAuthorizationHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
    {
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, QuizOwnerRequirement requirement, Guid Id)
    {
        var userId = _httpContextAccessor.GetUserId();
        var quiz = await _quizRepository.Get(Id);

        Console.WriteLine($"userId = {userId}");
        Console.WriteLine($"quizId = {Id}");
        Console.WriteLine($"quizOwnerId = {quiz?.OwnerId}");

        if (quiz != null && quiz.OwnerId.ToString().Equals(userId))
        {
            Console.WriteLine($"userId = {userId}");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        Console.WriteLine($"userId = {userId}");
        Console.WriteLine($"userId = {quiz?.OwnerId}");
        await Task.CompletedTask;
    }
}
