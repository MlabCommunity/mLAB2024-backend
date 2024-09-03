using FluentValidation;

namespace QuizBackend.Application.Commands.Quizzes.UpdateQuiz
{
    public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
    {
        public UpdateQuizCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.");
        }
    }
}
