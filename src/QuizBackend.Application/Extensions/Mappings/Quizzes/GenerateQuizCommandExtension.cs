using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

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

        throw new BadRequestException("Invalid question type list. Must contain at least one valid question type.");
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
            _ => throw new BadRequestException("Invalid language selected. Must select one valid language."),
        };
    }
}