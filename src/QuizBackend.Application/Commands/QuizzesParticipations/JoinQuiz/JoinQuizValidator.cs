using FluentValidation;

namespace QuizBackend.Application.Commands.QuizzesParticipations.JoinQuiz;
public class JoinQuizValidator : AbstractValidator<JoinQuizCommand>
{
    public JoinQuizValidator()
    {
        RuleFor(x => x.JoinCode)
           .MaximumLength(8).WithMessage("Join code cannot be longer than 8 characters.")
           .Matches("^[a-zA-Z0-9]*$").WithMessage("Join Code can only include letters or digits.");
    }
}
