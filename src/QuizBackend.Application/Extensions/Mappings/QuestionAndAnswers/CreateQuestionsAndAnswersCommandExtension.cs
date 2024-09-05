using QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;

public static class CreateQuestionsAndAnswersCommandExtension
{
    public static Question ToEntity(this CreateQuestionAndAnswersCommand command, IDateTimeProvider dateTimeProvider)
    {
        return new Question
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            QuizId = command.QuizId,
            CreatedAtUtc = dateTimeProvider.UtcNow,
            Answers = command.CreateAnswers.Select(a => new Answer
            {
                Id = Guid.NewGuid(),
                Content = a.Content,
                IsCorrect = a.IsCorrect,
                CreatedAtUtc = dateTimeProvider.UtcNow
            }).ToList()
        };
    }
}