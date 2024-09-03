using FluentValidation;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers
{
    public class CreateQuestionAndAnswersCommandValidator : AbstractValidator<CreateQuestionAndAnswersCommand>
    {
        public CreateQuestionAndAnswersCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.CreateQuestionAnswers)
                .NotEmpty().WithMessage("At least one question and answer pair is required.")
                .Must(qa => qa.Count >= 2).WithMessage("You must provide at least 2 question-answer pairs.");

            RuleForEach(x => x.CreateQuestionAnswers).SetValidator(new CreateQuestionAnswerValidator());

            RuleFor(x => x.QuizId)
                .NotEmpty().WithMessage("QuizId is required.");
        }
    }

    public class CreateQuestionAnswerValidator : AbstractValidator<CreateQuestionAnswer>
    {
        public CreateQuestionAnswerValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(250).WithMessage("Content must be at most 250 characters.");

            RuleFor(x => x.IsCorrect)
                .NotNull().WithMessage("IsCorrect must be specified.");
        }
    }
}