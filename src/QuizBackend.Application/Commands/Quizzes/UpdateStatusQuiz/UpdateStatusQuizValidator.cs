using FluentValidation;
using QuizBackend.Application.Commands.UpdateStatusQuiz;

namespace QuizBackend.Application.Commands.Quizzes.UpdateStatusQuiz
{
    public class UpdateStatusQuizValidator : AbstractValidator<UpdateStatusQuizCommand>
    {
        public UpdateStatusQuizValidator()
        {
            RuleFor(q => q.Status)
               .NotEmpty().WithMessage("Status is required.")
               .IsInEnum().WithMessage("Invalid status value. Allowed values are: 'Active', 'Inactive'.");
        }
    }
}