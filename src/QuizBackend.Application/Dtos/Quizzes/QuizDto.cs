using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Dtos.Quizzes;

public record QuizDto(
    Guid Id,
    string Title,
    string? Description,
    Availability Availability,
    Status Status,
    int TotalQuestions);