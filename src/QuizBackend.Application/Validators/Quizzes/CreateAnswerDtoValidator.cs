using FluentValidation;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;

namespace QuizBackend.Application.Validators.Quizzes
{
    public class CreateAnswerDtoValidator : AbstractValidator<CreateAnswerDto>
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
