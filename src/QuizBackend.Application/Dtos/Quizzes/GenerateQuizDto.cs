namespace QuizBackend.Application.Dtos.Quizzes;

public record GenerateQuizResponse(string Title, string Description, List<GenerateQuestion> GenerateQuestions);
public record GenerateQuestion(string Title, List<GenerateAnswer> GenerateAnswers);
public record GenerateAnswer(string Content, bool IsCorrect);