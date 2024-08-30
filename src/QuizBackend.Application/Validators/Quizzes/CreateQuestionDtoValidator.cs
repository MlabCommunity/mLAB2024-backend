using FluentValidation;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;

namespace QuizBackend.Application.Validators.Quizzes
{
    public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Question title is required")
                .MaximumLength(100).WithMessage("Question title must be at most 100 characters");

            RuleFor(x => x.CreateAnswersDto)
                .NotEmpty().WithMessage("At least one answer is required")
                .Must(a => a.Count > 2).WithMessage("Each question must have at least 2 answers")
                .Must(a => a.Count <= 4).WithMessage("Each question have a maximum of 4 answers");

            RuleForEach(x => x.CreateAnswersDto).SetValidator(new CreateAnswerDtoValidator());
        }
    }
}
