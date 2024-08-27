using FluentValidation;
using QuizBackend.Application.Dtos.CreateQuiz;

namespace QuizBackend.Application.Validators
{
    public class QuizArgumentsDtoValidator : AbstractValidator<QuizArgumentsDto>
    {
        public QuizArgumentsDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.NumberOfQuestions)
                .GreaterThan(0).WithMessage("Number of questions must be greater than 0");

            RuleFor(x => x.TypeOfQuestions)
                .NotEmpty().WithMessage("Type of questions is required")
                .Must(type => type == "multiple choices" || type == "true/false")
                .WithMessage("Type of questions must be either 'multiple choices' or 'true/false'");
        }
    }
}
