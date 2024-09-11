using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.QuizResults;

public static class QuizResultExtension
{
   public static QuizResult ToEntity(this QuizResult quizResult, IDateTimeProvider dateTimeProvider)
    {
        return new QuizResult
        {
            QuizParticipationId = quizResult.QuizParticipationId,
            TotalQuestions = quizResult.TotalQuestions,
            CorrectAnswers = quizResult.CorrectAnswers,
            ScorePercentage = quizResult.ScorePercentage,
            CalculatedAt = dateTimeProvider.UtcNow
        };
    }
}