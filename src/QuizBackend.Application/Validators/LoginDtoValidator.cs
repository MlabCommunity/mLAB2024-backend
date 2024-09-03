using FluentValidation;
using QuizBackend.Application.Dtos.Auth;

namespace QuizBackend.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
