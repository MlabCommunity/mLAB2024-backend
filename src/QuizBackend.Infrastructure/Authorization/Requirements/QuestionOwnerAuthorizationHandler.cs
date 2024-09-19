using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Infrastructure.Authorization.Requirements;

public class QuestionOwnerAuthorizationHandler : AuthorizationHandler<QuestionOwnerRequirement, Guid>
{
    private readonly IQuestionAndAnswersRepository _questionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuestionOwnerAuthorizationHandler(IQuestionAndAnswersRepository questionRepository, IHttpContextAccessor httpContextAccessor)
    {
        _questionRepository = questionRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, QuestionOwnerRequirement requirement, Guid questionId)
    {
        var userId = _httpContextAccessor.GetUserId();

        var quiz = await _questionRepository.GetQuizForQuestion(questionId);
        if (quiz != null && quiz.OwnerId == userId)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        await Task.CompletedTask;
    }
}