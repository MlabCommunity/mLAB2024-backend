using QuizBackend.Application.Commands.Quizzes.CreateQuiz;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
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
                Title = command.Title,
                Description = command.Description,
                OwnerId = ownerId,
                CreatedAtUtc = DateTime.UtcNow,
                Questions = command.Questions.Select(q => new Question
                {
                    Id = Guid.NewGuid(),
                    Title = q.Title,
                    CreatedAtUtc = DateTime.UtcNow,
                    Answers = q.Answers.Select(a => new Answer
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

