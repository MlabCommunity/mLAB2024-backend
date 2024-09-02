using FluentValidation;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters");

            RuleFor(x => x.CreateQuestions)
                .NotEmpty().WithMessage("At least one question is required")
                .Must(q => q.Count <= 15).WithMessage("Quiz cannot have more than 15 questions");

            RuleForEach(x => x.CreateQuestions).ChildRules(questions =>
            {
                questions.RuleFor(q => q.Title)
                    .NotEmpty().WithMessage("Question title is required")
                    .MaximumLength(100).WithMessage("Question title must be at most 100 characters");

                questions.RuleFor(q => q.CreateAnswers)
                    .NotEmpty().WithMessage("At least one answer is required")
                    .Must(a => a.Count >= 2).WithMessage("Each question must have at least 2 answers")
                    .Must(a => a.Count <= 4).WithMessage("Each question can have a maximum of 4 answers");

                questions.RuleForEach(q => q.CreateAnswers).ChildRules(answers =>
                {
                    answers.RuleFor(a => a.Content)
                        .NotEmpty().WithMessage("Answer content is required")
                        .MaximumLength(250).WithMessage("Answer content must be at most 250 characters");

                    answers.RuleFor(a => a.IsCorrect)
                        .NotNull().WithMessage("IsCorrect must be specified");
                });
            });
        }
    }
}
