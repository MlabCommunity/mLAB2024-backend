using FluentValidation;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;

public class CreateQuestionAndAnswersCommandValidator : AbstractValidator<CreateQuestionAndAnswersCommand>
{
    public CreateQuestionAndAnswersCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

        RuleFor(x => x.CreateAnswers)
            .NotEmpty().WithMessage("At least one question and answer pair is required.")
            .Must(a => a.Count >= 2).WithMessage("You must provide at least 2 question-answer pairs.");

        RuleFor(x => x.CreateAnswers)
           .Must(HaveAtLeastOneCorrectAnswer)
           .WithMessage("At least one answer must be marked as correct.");

        RuleForEach(x => x.CreateAnswers).SetValidator(new CreateAnswerValidator());

        RuleFor(x => x.QuizId)
            .NotEmpty().WithMessage("QuizId is required.");
    }
    private bool HaveAtLeastOneCorrectAnswer(List<CreateAnswer> createAnswers)
    {
        return createAnswers.Any(a => a.IsCorrect);
    }
}

public class CreateAnswerValidator : AbstractValidator<CreateAnswer>
{
    public CreateAnswerValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(250).WithMessage("Content must be at most 250 characters.");

        RuleFor(x => x.IsCorrect)
            .NotNull().WithMessage("IsCorrect must be specified.");
    }
}