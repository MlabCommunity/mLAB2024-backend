namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class GenerateQuestionDto
    {
        public string Title { get; set; }
        public List<GenerateAnswerDto> GenerateAnswersDto { get; set; }
    }
}
