using QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;

public static class UpdateQuestionsAndAnswersCommandExtension
{
    public static void UpdateEntity(this UpdateQuestionAndAnswersCommand command, Question question, IDateTimeProvider dateTimeProvider)
    {
        question.Title = command.Title;
        question.UpdatedAtUtc = dateTimeProvider.UtcNow;

        foreach (var updatedAnswer in command.UpdateAnswers)
        {
            var answer = question.Answers.FirstOrDefault(a => a.Id == updatedAnswer.Id) ??
                throw new NotFoundException(nameof(Answer), updatedAnswer.Id.ToString());
           
                answer.Content = updatedAnswer.Content;
                answer.IsCorrect = updatedAnswer.IsCorrect;
                answer.UpdatedAtUtc = dateTimeProvider.UtcNow;
        }
    }
}