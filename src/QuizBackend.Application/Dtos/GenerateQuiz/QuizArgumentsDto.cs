
namespace QuizBackend.Application.Dtos.CreateQuiz
{
    public class QuizArgumentsDto
    {
        public string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public string TypeOfQuestions { get; set; }
    }
}
