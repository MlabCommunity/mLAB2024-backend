using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace QuizBackend.Application.Commands.Quizzes.JoinQuiz;
public class JoinQuizValidator : AbstractValidator<JoinQuizCommand>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JoinQuizValidator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.UserName)
           .Custom((username, context) =>
           {
               if (!IsUserLoggedIn() && string.IsNullOrWhiteSpace(username))
               {
                   context.AddFailure("Display name is required if not logged in.");
               }
           })
           .MaximumLength(50).WithMessage("Display name cannot be longer than 50 characters.")
           .Matches("^[a-zA-Z0-9]*$").WithMessage("Display name can only contain letters and digits.");
    }

    private bool IsUserLoggedIn()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.Identity != null && user.Identity.IsAuthenticated;
    }
}
