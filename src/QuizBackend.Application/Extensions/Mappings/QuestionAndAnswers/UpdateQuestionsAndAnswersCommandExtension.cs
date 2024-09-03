using QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers
{
    public static class UpdateQuestionsAndAnswersCommandExtension
    {
        public static void UpdateEntity(this UpdateQuestionAndAnswersCommand command, Question question)
        {
            question.Title = command.Title;
            question.UpdatedAtUtc = DateTime.UtcNow;

            foreach (var updatedAnswer in command.UpdateQuestionAnswers)
            {
                var answer = question.Answers.FirstOrDefault(a => a.Id == updatedAnswer.Id);
                if (answer != null)
                {
                    answer.Content = updatedAnswer.Content;
                    answer.IsCorrect = updatedAnswer.IsCorrect;
                    answer.UpdatedAtUtc = DateTime.UtcNow;
                }
                else
                {
                    throw new NotFoundException(nameof(Answer), updatedAnswer.Id.ToString());
                }
            }
        }
    }
}