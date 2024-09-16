using FluentValidation;

namespace QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;

public class SubmitQuizAnswerCommandValidator : AbstractValidator<SubmitQuizAnswerCommand>
{
    public SubmitQuizAnswerCommandValidator()
    {
        RuleFor(x => x.QuizParticipationId)
            .NotEmpty().WithMessage("QuizParticipationId is required.");

        RuleFor(x => x.QuestionsId)
            .NotEmpty().WithMessage("QuestionsId cannot be empty.")
            .Must(HaveSameCountAsAnswers).WithMessage("The number of QuestionsId must match the number of AnswersId.");

        RuleFor(x => x.AnswersId)
            .NotEmpty().WithMessage("AnswersId cannot be empty.")
            .Must(HaveSameCountAsQuestions).WithMessage("The number of AnswersId must match the number of QuestionsId.");

        RuleForEach(x => x.QuestionsId)
            .NotEmpty().WithMessage("Each QuestionId must be provided.");

        RuleForEach(x => x.AnswersId)
            .NotEmpty().WithMessage("Each AnswerId must be provided.");
    }

    private bool HaveSameCountAsAnswers(SubmitQuizAnswerCommand command, List<Guid> questionsId)
    {
        return questionsId.Count == command.AnswersId.Count;
    }

    private bool HaveSameCountAsQuestions(SubmitQuizAnswerCommand command, List<Guid> answersId)
    {
        return answersId.Count == command.QuestionsId.Count;
    }
}