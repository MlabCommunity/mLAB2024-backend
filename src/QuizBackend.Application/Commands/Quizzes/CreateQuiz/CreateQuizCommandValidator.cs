using FluentValidation;
using QuizBackend.Application.Validators.Quizzes;

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

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId is required");

            RuleFor(x => x.Questions)
                .NotEmpty().WithMessage("At least one question is required")
                .Must(q => q.Count <= 15).WithMessage("Quiz cannot have more than 15 questions");

            RuleForEach(x => x.Questions).ChildRules(questions =>
            {
                questions.RuleFor(q => q.Title)
                    .NotEmpty().WithMessage("Question title is required")
                    .MaximumLength(100).WithMessage("Question title must be at most 100 characters");

                questions.RuleFor(q => q.Answers)
                    .NotEmpty().WithMessage("At least two answers are required")
                    .Must(a => a.Count >= 2).WithMessage("At least two answers are required")
                    .Must(a => a.Count <= 4).WithMessage("Question cannot have more than four answers");

                questions.RuleFor(q => q.QuizId)
                    .NotEmpty().WithMessage("QuizId is required");

                questions.RuleForEach(q => q.Answers).ChildRules(answers =>
                {
                    answers.RuleFor(a => a.Content)
                        .NotEmpty().WithMessage("Answer content is required")
                        .MaximumLength(250).WithMessage("Answer content must be at most 250 characters");

                    answers.RuleFor(a => a.QuestionId)
                        .NotEmpty().WithMessage("QuestionId is required");
                });
            });
        }
    }
}
