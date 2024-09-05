using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

public static class CreateQuizCommandExtension
{
    public static Quiz ToEntity(this CreateQuizCommand command, string ownerId, IDateTimeProvider dateTimeProvider)
    {
        return new Quiz
        {
            Id = Guid.NewGuid(),
            Description = command.Description,
            Title = command.Title,
            OwnerId = ownerId,
            CreatedAtUtc = dateTimeProvider.UtcNow,
            Questions = command.CreateQuizQuestions.Select(q => new Question
            {
                Id = Guid.NewGuid(),
                Title = q.Title,
                CreatedAtUtc = dateTimeProvider.UtcNow,
                Answers = q.CreateQuizAnswers.Select(a => new Answer
                {
                    Id = Guid.NewGuid(),
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList()
        };
    }
}