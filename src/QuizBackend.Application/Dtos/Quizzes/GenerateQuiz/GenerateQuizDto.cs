namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class GenerateQuizDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required List<GenerateQuestionDto> GenerateQuestionsDto { get; set; }
    }
}
