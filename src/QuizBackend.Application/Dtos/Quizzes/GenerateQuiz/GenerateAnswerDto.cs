namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class GenerateAnswerDto
    {
        public required string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
