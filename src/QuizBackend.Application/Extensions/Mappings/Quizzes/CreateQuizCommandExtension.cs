using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes
{
    public static class CreateQuizCommandExtension
    {
        public static Quiz ToEntity(this CreateQuizCommand command, string ownerId)
        {
            return new Quiz
            {
                Id = Guid.NewGuid(),
                Description = command.Description,
                Title = command.Title,
                OwnerId = ownerId,
                CreatedAtUtc = DateTime.UtcNow,
                Questions = command.CreateQuestions.Select(q => new Question
                {
                    Id = Guid.NewGuid(),
                    Title = q.Title,
                    CreatedAtUtc = DateTime.UtcNow,
                    Answers = q.CreateAnswers.Select(a => new Answer
                    {
                        Id = Guid.NewGuid(),
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };
        }
    }
}

