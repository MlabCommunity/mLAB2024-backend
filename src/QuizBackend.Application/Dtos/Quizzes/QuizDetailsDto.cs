using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Dtos.Quizzes
{
    public record QuizDetailsDto(
        Guid Id,
        string Title,
        string? Description,
        Availability Availability,
        Status Status,
        List<QuestionDto> Questions);
}