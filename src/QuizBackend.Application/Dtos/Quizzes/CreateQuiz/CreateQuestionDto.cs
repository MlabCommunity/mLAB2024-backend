
namespace QuizBackend.Application.Dtos.Quizzes.CreateQuiz
{
    public class CreateQuestionDto
    {
        public required string Title { get; set; }
        public List<CreateAnswerDto> CreateAnswersDto { get; set; }
    }
}
