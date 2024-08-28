
namespace QuizBackend.Application.Dtos.Quizzes.CreateQuiz
{
    public class CreateQuestionDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid QuizId { get; set; }
        public required List<CreateAnswerDto> CreateAnswersDto { get; set; }
    }
}
