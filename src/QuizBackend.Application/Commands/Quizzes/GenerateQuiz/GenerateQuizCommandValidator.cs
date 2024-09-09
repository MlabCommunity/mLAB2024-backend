using FluentValidation;
using Microsoft.AspNetCore.Http;
using QuizBackend.Domain.Enums;
namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz;

public class GenerateQuizCommandValidator : AbstractValidator<GenerateQuizCommand>
{
    private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
    private const int MaxFilesCount = 3;
    private static readonly string[] AllowedExtensions = [".pdf", ".docx", ".xlsx", ".txt", ".pptx"];

    public GenerateQuizCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");

        RuleFor(x => x.NumberOfQuestions)
            .GreaterThan(0).WithMessage("Number of questions must be greater than 0");

        RuleFor(x => x.QuestionTypes)
            .Must(ContainValidQuestionTypes).WithMessage("Type of questions must be either 'MultipleChoice' or 'TrueFalse'")
            .When(x => x.QuestionTypes != null);

        RuleFor(x => x.Attachments)
            .Must(BeValidFiles).WithMessage($"Invalid file format or size or too many uploaded files {MaxFilesCount}")
            .When(x => x.Attachments != null);
    }
    private bool ContainValidQuestionTypes(List<QuestionType> questionTypes)
    {
        return questionTypes.All(type => Enum.IsDefined(typeof(QuestionType), type));
    }
    private bool BeValidFiles(List<IFormFile>? files)
    {
        if (files == null || files.Count == 0 || files.Count > MaxFilesCount)
        {
            return false; 
        }
        return files.All(file =>
            file.Length > 0 &&
            file.Length <= MaxFileSize &&
            AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()));
    }
}