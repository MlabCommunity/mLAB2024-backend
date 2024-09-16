using QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.QuizParticipation;

public static class SubmitQuizAnswerCommandExtension
{
    public static List<UserAnswer> ToEntities(this SubmitQuizAnswerCommand command, IDateTimeProvider dateTimeProvider)
    {
        var userAnswers = new List<UserAnswer>();

        for (int i = 0; i < command.QuestionsId.Count; i++)
        {
            userAnswers.Add(new UserAnswer
            {
                QuizParticipationId = command.QuizParticipationId,
                QuestionId = command.QuestionsId[i],
                AnswerId = command.AnswersId[i],
                CreatedAtUtc = dateTimeProvider.UtcNow
            });
        }
        return userAnswers;
    }
}