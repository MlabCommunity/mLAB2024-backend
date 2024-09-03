using QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers
{
    public static class CreateQuestionsAndAnswersCommandExtension
    {
        public static Question ToEntity(this CreateQuestionAndAnswersCommand command)
        {
            return new Question
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                QuizId = command.QuizId,
                CreatedAtUtc = DateTime.UtcNow,
                Answers = command.CreateQuestionAnswers.Select(a => new Answer
                {
                    Id = Guid.NewGuid(),
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                    CreatedAtUtc = DateTime.UtcNow
                }).ToList()
            };
        }
    }
}