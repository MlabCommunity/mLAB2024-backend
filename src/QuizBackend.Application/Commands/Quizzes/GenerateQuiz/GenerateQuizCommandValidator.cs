using FluentValidation;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public class GenerateQuizCommandValidator : AbstractValidator<GenerateQuizCommand>
    {
        public GenerateQuizCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.NumberOfQuestions)
                .GreaterThan(0).WithMessage("Number of questions must be greater than 0");

            RuleFor(x => x.QuestionType)
                .IsInEnum().WithMessage("Invalid question type.")
                .WithMessage("Type of questions must be either 'multiple choices' or 'true/false'");
        }
    }
}
