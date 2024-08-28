using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes
{
    public static class UpdateQuestionDtoExtension
    {
        public static Question ToEntity(this UpdateQuestionDto dto)
        {
            return new Question
            {
                Title = dto.Title,
                Description = dto.Description,
            };
        }
    }
}
