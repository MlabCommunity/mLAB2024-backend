namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class QuizArgumentsDto
    {
        public required string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public required string TypeOfQuestions { get; set; }
    }
}
