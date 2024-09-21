using Microsoft.SemanticKernel;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Infrastructure.Interfaces;

public interface IChatHistoryService
{
    void AddAssistantMessage(string content);
    void AddSystemMessage(string content);
    void AddUserMessage(string content);
    void AddQuizGenerationDetails(string content, int numberOfQuestions, List<QuestionType> questionTypes, string quizResult);
    void ClearHistory();
    IReadOnlyList<ChatMessageContent> GetHistory();
}