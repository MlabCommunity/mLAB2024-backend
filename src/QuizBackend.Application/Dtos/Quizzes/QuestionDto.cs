namespace QuizBackend.Application.Dtos.Quizzes;

public record QuestionDto(
    Guid Id,
    string Title,
    List<AnswerDto> Answers);