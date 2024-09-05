using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz;

public record GenerateQuizResponse(string Title, string Description, List<GenerateQuestion> GenerateQuestions);
public record GenerateQuestion(string Title, List<GenerateAnswer> GenerateAnswers);
public record GenerateAnswer(string Content, bool IsCorrect);
public class GenerateQuizCommandHandler : ICommandHandler<GenerateQuizCommand, GenerateQuizResponse>
{
    private readonly IQuizService _quizService;
    private readonly IAttachmentProcessor _attachmentProcessor;
    private const int MaxContentLength = 10000;

    public GenerateQuizCommandHandler(IQuizService quizService, IAttachmentProcessor attachmentProcessor)
    {
        _quizService = quizService;
        _attachmentProcessor = attachmentProcessor;
    }

    public async Task<GenerateQuizResponse> Handle(GenerateQuizCommand command, CancellationToken cancellationToken)
    {
        string content = command.Content;
        var processedAttachments = await _attachmentProcessor.ProcessAttachments(command.Attachments!);
        content = $"{content}\n\n{string.Join("\n\n", processedAttachments)}";
        
        if (content.Length > MaxContentLength)
        {
            content = content.Substring(0, MaxContentLength);
        }

        var updatedCommand = command with { Content = content };
        var quizDto = await _quizService.GenerateQuizFromPromptTemplateAsync(command);
        return quizDto;
    }
}