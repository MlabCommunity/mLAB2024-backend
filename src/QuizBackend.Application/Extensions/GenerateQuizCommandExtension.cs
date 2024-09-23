using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Extensions;

public static class GenerateQuizCommandExtension
{
    public static string GetQuestionTypeString(this GenerateQuizCommand command)
    {
        bool containsSingleChoice = command.QuestionTypes.Contains(QuestionType.SingleChoice);
        bool containsTrueFalse = command.QuestionTypes.Contains(QuestionType.TrueFalse);

        if (containsSingleChoice && containsTrueFalse)
        {
            return "SingleChoice+TrueFalse";
        }
        else if (containsSingleChoice)
        {
            return "SingleChoice";
        }
        else if (containsTrueFalse)
        {
            return "TrueFalse";
        }

        throw new ArgumentException("Invalid question type list. Must contain at least one valid question type.");
    }

    public static string GetSelectedLanguageString(this GenerateQuizCommand command)
    {
        return command.Language switch
        {
            QuizLanguage.English => "English",
            QuizLanguage.Polish => "Polish",
            QuizLanguage.German => "German",
            QuizLanguage.Spanish => "Spanish",
            QuizLanguage.French => "French",
            QuizLanguage.Italian => "Italian",
            _ => throw new ArgumentException("Invalid language selected. Must select one valid language."),
        };
    }
}