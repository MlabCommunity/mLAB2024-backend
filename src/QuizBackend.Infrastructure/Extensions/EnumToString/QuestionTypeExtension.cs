using QuizBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuizBackend.Infrastructure.Extensions.EnumToString;

public static class QuestionTypeExtension
{
    public static string GetQuestionTypeString(QuestionType questionType)
    {
        if (questionType.HasFlag(QuestionType.SingleChoice) && questionType.HasFlag(QuestionType.TrueFalse))
        {
            return "SingleChoice+TrueFalse";
        }
        else if (questionType.HasFlag(QuestionType.SingleChoice))
        {
            return "SingleChoice";
        }
        else if (questionType.HasFlag(QuestionType.TrueFalse))
        {
            return "TrueFalse";
        }

        throw new ValidationException("Invalid question type.");
    }
}