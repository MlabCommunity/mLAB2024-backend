
namespace QuizBackend.Application.Dtos.Quiz
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CreateAnswerDto> AnswersDto { get; set; }
        public CreateQuizDto Quiz { get; set; }
    }
}
