namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class GenerateQuizDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GenerateQuestionDto> GenerateQuestionsDto { get; set; }
    }
}
