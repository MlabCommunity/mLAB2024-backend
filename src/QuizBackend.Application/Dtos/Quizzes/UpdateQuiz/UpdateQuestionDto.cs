
namespace QuizBackend.Application.Dtos.Quizzes.UpdateQuiz
{
    public class UpdateQuestionDto
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
       
    }
}
