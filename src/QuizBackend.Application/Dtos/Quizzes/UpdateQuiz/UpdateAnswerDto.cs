
namespace QuizBackend.Application.Dtos.Quizzes.UpdateQuiz
{
    public class UpdateAnswerDto
    {
        public required string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
