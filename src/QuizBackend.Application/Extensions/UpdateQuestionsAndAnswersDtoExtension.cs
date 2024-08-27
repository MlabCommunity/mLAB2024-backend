
using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions
{
    public static class UpdateQuestionsAndAnswersDtoExtension
    {
        public static Question ToEntity(this UpdateQuestionDto dto)
        {
            return new Question
            {
                Title = dto.Title,
                Description = dto.Description,
                Answers = dto.UpdateAnswersDto.Select(a => new Answer
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                }).ToList()
            };
        }
    }
}
