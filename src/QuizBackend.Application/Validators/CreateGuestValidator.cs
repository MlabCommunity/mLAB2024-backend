using FluentValidation;
using QuizBackend.Application.Dtos.Auth;

namespace QuizBackend.Application.Validators;

public class CreateGuestValidator : AbstractValidator<CreateGuestRequest>
{
    public CreateGuestValidator()
    {
        RuleFor(request => request.DisplayName)
            .NotEmpty().WithMessage("Display name is required.")
            .MaximumLength(50).WithMessage("Display name cannot be longer than 50 characters.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Display name can only contain letters and digits.");
    }
}