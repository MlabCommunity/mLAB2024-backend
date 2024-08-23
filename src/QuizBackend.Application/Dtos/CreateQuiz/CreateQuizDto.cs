
namespace QuizBackend.Application.Dtos.Quiz
{
    public class CreateQuizDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CreateQuestionDto> QuestionsDto { get; set; } 
    }
}
