
namespace QuizBackend.Application.Dtos.Quizzes.CreateQuiz
{
    public class CreateAnswerDto
    {
        public required string Content { get; set; }
        public bool IsCorrect { get; set; }
        public required Guid QuestionId { get; set; }
    }
}
