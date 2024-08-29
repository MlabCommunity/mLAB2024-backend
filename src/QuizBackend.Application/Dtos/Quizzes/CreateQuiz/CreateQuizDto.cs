
namespace QuizBackend.Application.Dtos.Quizzes.CreateQuiz
{
    public class CreateQuizDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        
    }
}
