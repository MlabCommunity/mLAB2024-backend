using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Answers
{
    public static class CreateAnswerDtoExtension
    {
        public static Answer ToEntity(this CreateAnswerDto dto)
        {
            return new Answer
            {
                Id = Guid.NewGuid(),
                Content = dto.Content,
                IsCorrect = dto.IsCorrect,
            };
        }
    }
}
