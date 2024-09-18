using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using QuizBackend.Domain.Enums;
using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Infrastructure.Services.AI;

public class ChatHistoryService : IChatHistoryService
{
    private readonly ChatHistory _chatHistory;

    public ChatHistoryService()
    {
        _chatHistory = [];
    }

    public void AddSystemMessage(string content)
    {
        _chatHistory.AddSystemMessage(content);
    }

    public void AddUserMessage(string content)
    {
        _chatHistory.AddUserMessage(content);
    }

    public void AddAssistantMessage(string content)
    {
        _chatHistory.AddAssistantMessage(content);
    }

    public void AddQuizGenerationDetails(string content, int numberOfQuestions, List<QuestionType> questionTypes, string quizResult)
    {
        var systemMessage = "Quiz generation details:";
        _chatHistory.AddSystemMessage(systemMessage);

        var userMessage = $"Content: {content}\nNumber of Questions: {numberOfQuestions}\nType of Questions: {questionTypes}";
        _chatHistory.AddUserMessage(userMessage);

        var assistantMessage = $"Generated Quiz: {quizResult}";
        _chatHistory.AddAssistantMessage(assistantMessage);
    }

    public IReadOnlyList<ChatMessageContent> GetHistory()
    {
        return _chatHistory.ToList();
    }

    public void ClearHistory()
    {
        _chatHistory.Clear();
    }
}