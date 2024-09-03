
namespace QuizBackend.Application.Dtos.Quizzes
{
    public record QuestionDto(
        Guid Id,
        string Title,
        string? Description,
        List<AnswerDto> Answers);
}