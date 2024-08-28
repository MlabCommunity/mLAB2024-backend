using FluentValidation;
using QuizBackend.Application.Validators.Quizzes;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            RuleFor(x => x.quizDto)
                .NotNull().WithMessage("QuizDto cannot be null");

            When(x => x.quizDto != null, () =>
            {
                RuleFor(x => x.quizDto.Title)
                    .NotEmpty().WithMessage("Title is required")
                    .MaximumLength(100).WithMessage("Title must be at most 100 characters");

                RuleFor(x => x.quizDto.Description)
                    .MaximumLength(500).WithMessage("Description must be at most 500 characters");

                RuleFor(x => x.quizDto.CreateQuestionsDto)
                    .NotEmpty().WithMessage("At least one question is required")
                    .Must(q => q.Count <= 15).WithMessage("Quiz cannot have more than 15 questions");

                RuleForEach(x => x.quizDto.CreateQuestionsDto).SetValidator(new CreateQuestionDtoValidator());
            });
        }
    }
}
