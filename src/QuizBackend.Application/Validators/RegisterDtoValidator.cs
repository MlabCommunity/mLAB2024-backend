using FluentValidation;
using QuizBackend.Application.Dtos;

namespace QuizBackend.Application.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
               .Must(HaveDigit).WithMessage("Password must contain at least one digit.")
               .Must(HaveLowercase).WithMessage("Password must contain at least one lowercase letter.")
               .Must(HaveUppercase).WithMessage("Password must contain at least one uppercase letter.")
               .Must(HaveNonAlphanumeric).WithMessage("Password must contain at least one special character.");
        }
        private bool HaveDigit(string password) => password.Any(char.IsDigit);

        private bool HaveLowercase(string password) => password.Any(char.IsLower);

        private bool HaveUppercase(string password) => password.Any(char.IsUpper);

        private bool HaveNonAlphanumeric(string password) => password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}
