using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI;

public class QuizService : IQuizService
{
    private readonly IKernelService _kernelService;
    private readonly ILogger<QuizService> _logger;
    private readonly IChatHistoryService _chatHistoryService;

    public QuizService(IKernelService kernelService, ILogger<QuizService> logger, IChatHistoryService chatHistoryService)
    {
        _kernelService = kernelService;
        _logger = logger;
        _chatHistoryService = chatHistoryService;
    }
    public async Task<GenerateQuizResponse> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command)
    {
        string questionTypeString = command.GetQuestionTypeString();

        var kernelArguments = new KernelArguments
        {
            {"content", command.Content },
            {"numberOfQuestions", command.NumberOfQuestions},
            {"typeOfQuestions", questionTypeString}
        };

        var jsonResponse = await _kernelService.CreatePluginFromPromptDirectory("GenerateQuiz", kernelArguments);
        var jsonStartIndex = jsonResponse.IndexOf('{');
        var jsonEndIndex = jsonResponse.LastIndexOf('}') + 1;
        var validJson = jsonResponse.Substring(jsonStartIndex, jsonEndIndex - jsonStartIndex);

        var quizDto = JsonConvert.DeserializeObject<GenerateQuizResponse>(validJson);

        if (string.IsNullOrWhiteSpace(validJson) || quizDto is null)
        {
            _logger.LogWarning("Quiz generation failed: Content: {content}, NumberOfQuestions: {numberOfQuestions}, TypeOfQuestions: {typeOfQuestions}",
                command.Content, command.NumberOfQuestions, command.QuestionTypes);

            throw new BadRequestException("Try generating again");
        }

        //_chatHistoryService.AddQuizGenerationDetails(
        //    command.Content,
        //    command.NumberOfQuestions,
        //    command.QuestionTypes,
        //    validJson);

        return quizDto;
    }
}