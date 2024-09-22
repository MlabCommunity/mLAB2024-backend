using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuizBackend.Application.Cache;
using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI;

public class QuizService : IQuizService
{
    private readonly IKernelService _kernelService;
    private readonly ILogger<QuizService> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public QuizService(IKernelService kernelService, ILogger<QuizService> logger, IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
    {
        _kernelService = kernelService;
        _logger = logger;
        _memoryCache = memoryCache;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<GenerateQuizResponse> GenerateQuizFromPromptTemplate(GenerateQuizCommand command)
    {
        var prompts = _kernelService.ImportAllPlugins();
        var content = await _kernelService.InvokeAsync(prompts["SummaryContent"], new() { { "text", command.Content },
            {"language", command.GetSelectedLanguageString()} }); 

        var jsonResponse = await _kernelService.InvokeAsync(prompts["GenerateQuiz"], new() { {"content", content },
            {"numberOfQuestions", command.NumberOfQuestions},
            {"language", command.GetSelectedLanguageString()},
            {"typeOfQuestions", command.GetQuestionTypeString()} });

        var validJson = ValidJson(jsonResponse);

        var quizDto = JsonConvert.DeserializeObject<GenerateQuizResponse>(validJson);

        if (string.IsNullOrWhiteSpace(validJson) || quizDto is null)
        {
            _logger.LogWarning("Quiz generation failed: Content: {content}, NumberOfQuestions: {numberOfQuestions}, TypeOfQuestions: {typeOfQuestions}",
                command.Content, command.NumberOfQuestions, command.QuestionTypes);

            throw new BadRequestException("Try generating again");
        }
        _memoryCache.Set(GetCacheKey(), new QuizCacheData
        {
            Content = content,
            QuizResponse = validJson
        },TimeSpan.FromMinutes(30));

        return quizDto;
    }

    public async Task<GenerateQuizResponse> RegenerateQuizFromPromptTemplate()
    {
        if (_memoryCache.TryGetValue(GetCacheKey(), out QuizCacheData quizData))
        {
            var content = quizData.Content;
            var quizResponse = quizData.QuizResponse;

            var prompts = _kernelService.ImportAllPlugins();

            var jsonResponse = await _kernelService.InvokeAsync(prompts["RegenerateQuiz"], new()
        {
            { "content", content },
            { "quizResponse", quizResponse }
        });

            var validJson = ValidJson(jsonResponse);
            var quizDto = JsonConvert.DeserializeObject<GenerateQuizResponse>(validJson);

            if (string.IsNullOrWhiteSpace(validJson) || quizDto is null)
            {
                throw new BadRequestException("Try regenerating again");
            }

            return quizDto;
        }
        else
        {
            _logger.LogWarning("No quiz data found in cache for user");
            throw new BadRequestException("No quiz data available for regeneration.");
        }
    }

    private string ValidJson(string json)
    {
        var jsonStartIndex = json.IndexOf('{');
        var jsonEndIndex = json.LastIndexOf('}') + 1;

        return json.Substring(jsonStartIndex, jsonEndIndex - jsonStartIndex);
    }
    public string GetCacheKey()
    {
        var userId = _httpContextAccessor.GetUserId();
        var cacheKey = "QuizData_" + userId;

        return cacheKey;
    }


}