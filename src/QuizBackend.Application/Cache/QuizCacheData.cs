namespace QuizBackend.Application.Cache;

public class QuizCacheData
{
    public string Content { get; set; } = null!;
    public string QuizResponse { get; set; } = null!;
    public int NumberOfQuestions { get; set; }
    public string Language { get; set; } = null!;
    public string QuestionTypes { get; set; } = null!;
}