using FluentValidation;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz;

public class GenerateQuizCommandValidator : AbstractValidator<GenerateQuizCommand>
{
    public GenerateQuizCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");

        RuleFor(x => x.NumberOfQuestions)
            .GreaterThan(0).WithMessage("Number of questions must be greater than 0");

        RuleFor(x => x.QuestionTypes)
          .Must(ContainValidQuestionTypes).WithMessage("Type of questions must be either 'MultipleChoice' or 'TrueFalse'")
          .When(x => x.QuestionTypes != null);
    }
    private bool ContainValidQuestionTypes(List<QuestionType> questionTypes)
    {
        return questionTypes.All(type => Enum.IsDefined(typeof(QuestionType), type));
    }
}