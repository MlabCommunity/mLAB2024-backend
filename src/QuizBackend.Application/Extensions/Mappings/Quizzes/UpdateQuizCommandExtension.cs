using QuizBackend.Application.Commands.Quizzes.UpdateQuiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

public static class UpdateQuizCommandExtension
{
    public static void UpdateEntity(this UpdateQuizCommand command, Quiz quiz, IDateTimeProvider dateTimeProvider)
    {
        quiz.Title = command.Title;
        quiz.Description = command.Description;
        quiz.UpdatedAtUtc = dateTimeProvider.UtcNow;
    }
}