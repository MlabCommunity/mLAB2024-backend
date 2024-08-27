
namespace QuizBackend.Application.Dtos.Quizzes.UpdateQuiz
{
    public class UpdateQuestionDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public List<UpdateAnswerDto> UpdateAnswersDto { get; set; }
    }
}
