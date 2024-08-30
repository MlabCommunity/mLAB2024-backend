
using QuizBackend.Application.Commands.Questions.UpdateQuestion;
using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions
{
    public static class UpdateQuestionsAndAnswersDtoExtension
    {
        public static Question ToEntity(this UpdateQuestionCommand dto)
        {
            return new Question
            {
                Title = dto.Title,
                Answers = dto.UpdateAnswers.Select(a => new Answer
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                }).ToList()
            };
        }
    }
}
