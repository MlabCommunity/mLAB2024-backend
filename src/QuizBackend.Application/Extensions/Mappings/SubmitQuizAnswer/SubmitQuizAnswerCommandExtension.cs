using QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.UserAnswers;

public static class SubmitQuizAnswerCommandExtension
{
    public static UserAnswer ToEntity(this SubmitQuizAnswerCommand command, IDateTimeProvider dateTimeProvider)
    {
        return new UserAnswer
        {
            QuizParticipationId = command.QuizParticipationId,
            QuestionId = command.QuestionId,
            AnswerId = command.AnswerId,
        };
    }
}