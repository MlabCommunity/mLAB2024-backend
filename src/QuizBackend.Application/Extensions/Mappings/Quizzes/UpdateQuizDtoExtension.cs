using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes
{
    public static class UpdateQuizDtoExtension
    {
        public static Quiz ToEntity(this UpdateQuizDto dto)
        {
            return new Quiz
            {
                Title = dto.Title,
                Description = dto.Description,
            };
        }
    }
}
