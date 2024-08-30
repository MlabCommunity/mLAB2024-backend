using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes
{
    public static class CreateQuizDtoExtension
    {
        public static Quiz ToEntity(this CreateQuizDto dto, string ownerId)
        {
            return new Quiz
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                OwnerId = ownerId,
                CreatedAtUtc = DateTime.UtcNow,
                Questions = dto.CreateQuestionsDto.Select(q => new Question
                {
                    Id = Guid.NewGuid(),
                    Title = q.Title,
                    CreatedAtUtc = DateTime.UtcNow,
                    Answers = q.CreateAnswersDto.Select(a => new Answer
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

