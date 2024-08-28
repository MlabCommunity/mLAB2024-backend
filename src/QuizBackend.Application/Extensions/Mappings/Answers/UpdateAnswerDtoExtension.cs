using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Answers
{
    public static class UpdateAnswerDtoExtension
    {
        public static Answer ToEntity(this UpdateAnswerDto dto)
        {
            return new Answer
            {
                Content = dto.Content,
                IsCorrect = dto.IsCorrect,
            };
        }
    }
}
