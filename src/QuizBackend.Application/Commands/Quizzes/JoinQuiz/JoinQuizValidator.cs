using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace QuizBackend.Application.Commands.Quizzes.JoinQuiz;
public class JoinQuizValidator : AbstractValidator<JoinQuizCommand>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JoinQuizValidator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.JoinCode)
            .NotEmpty().WithMessage("Quiz code is required.")
            .MaximumLength(8).WithMessage("Quiz code cannot be longer than 8 characters.");

        RuleFor(x => x.UserName)
           .NotEmpty().When(x => !IsUserLoggedIn()).WithMessage("Display name is required if not logged in.")
           .MaximumLength(50).WithMessage("Display name cannot be longer than 50 characters.")
           .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Display name cannot be empty or whitespace.");
    }

    private bool IsUserLoggedIn()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return user?.Identity != null && user.Identity.IsAuthenticated;
    }
}
