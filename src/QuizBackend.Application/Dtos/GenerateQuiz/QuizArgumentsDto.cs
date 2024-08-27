
namespace QuizBackend.Application.Dtos.CreateQuiz
{
    public class QuizArgumentsDto
    {
        public required string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public required string TypeOfQuestions { get; set; }
    }
}
