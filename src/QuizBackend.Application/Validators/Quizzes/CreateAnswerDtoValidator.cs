using FluentValidation;
using QuizBackend.Application.Commands.Questions.CreateQuestion;

namespace QuizBackend.Application.Validators.Quizzes
{
    public class CreateAnswerDtoValidator : AbstractValidator<CreateAnswer>
    {
        public CreateAnswerDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Answer content is required")
                .MaximumLength(250).WithMessage("Answer content must be at most 250 characters");

            RuleFor(x => x.IsCorrect)
                .NotNull().WithMessage("IsCorrect must be specified");
        }
    }
}
