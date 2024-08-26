
namespace QuizBackend.Application.Dtos.Quiz
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }
        public List<CreateAnswerDto> CreateAnswersDto { get; set; }
    }
}
