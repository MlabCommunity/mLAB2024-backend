using FluentValidation;

namespace QuizBackend.Application.Commands.Quizzes.UpdateAvailability
{
    public class UpdateAvailabilityValidator : AbstractValidator<UpdateAvailabilityCommand>
    {
        public UpdateAvailabilityValidator()
        {
            RuleFor(q => q.Availability)
               .NotEmpty().WithMessage("Status is required.")
               .IsInEnum().WithMessage("Invalid status value. Allowed values are: 'Public', 'Private'.");
        }
        
    }
}
