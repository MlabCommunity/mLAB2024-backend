using FluentValidation;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers
{
    public class UpdateQuestionAndAnswersCommandValidator : AbstractValidator<UpdateQuestionAndAnswersCommand>
    {
        public UpdateQuestionAndAnswersCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.UpdateQuestionAnswers)
                .NotEmpty().WithMessage("At least one question and answer pair is required.")
                .Must(qa => qa.Count >= 1).WithMessage("You must provide at least 1 question-answer pair.");

            RuleForEach(x => x.UpdateQuestionAnswers).SetValidator(new UpdateQuestionAnswerValidator());
        }
    }

    public class UpdateQuestionAnswerValidator : AbstractValidator<UpdateQuestionAnswer>
    {
        public UpdateQuestionAnswerValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(250).WithMessage("Content must be at most 250 characters.");

            RuleFor(x => x.IsCorrect)
                .NotNull().WithMessage("IsCorrect must be specified.");
        }
    }
}
