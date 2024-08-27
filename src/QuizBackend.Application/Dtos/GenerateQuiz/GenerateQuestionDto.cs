
namespace QuizBackend.Application.Dtos.Quiz
{
    public class GenerateQuestionDto
    {
        public string Title { get; set; }
        public List<GenerateAnswerDto> GenerateAnswersDto { get; set; }
    }
}
