
namespace QuizBackend.Application.Dtos.Quizzes.UpdateQuiz
{
    public class UpdateAnswerDto
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public bool IsCorrect { get; set; }
        public required Guid QuestionId { get; set; }
    }
}
