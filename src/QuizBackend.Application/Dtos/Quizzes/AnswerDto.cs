
namespace QuizBackend.Application.Dtos.Quizzes
{
    public record AnswerDto(
        Guid Id,
        string Content,
        bool IsCorrect);
}