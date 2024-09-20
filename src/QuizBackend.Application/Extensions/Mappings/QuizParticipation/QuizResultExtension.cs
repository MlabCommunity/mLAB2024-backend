using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Dtos.UserAnswers;
using QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.QuizParticipation;

public static class QuizResultExtension
{
    public static QuizResultResponse ToResponse(
        this QuizResult quizResult,
        QuizBackend.Domain.Entities.QuizParticipation quizParticipation,
        List<QuestionDto> questionsDto,
        List<UserAnswerDto> userAnswersDto)
    {
        return new QuizResultResponse(
            quizParticipation.Id,
            new QuizDetails(
                quizParticipation.Quiz.Id,
                quizParticipation.Quiz.Title,
                quizParticipation.Quiz.Description,
                questionsDto
            ),
            userAnswersDto,
            quizResult.TotalQuestions,
            quizResult.CorrectAnswers,
            quizResult.ScorePercentage
        );
    }
}