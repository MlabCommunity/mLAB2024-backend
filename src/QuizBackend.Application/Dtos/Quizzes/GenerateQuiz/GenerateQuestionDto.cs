namespace QuizBackend.Application.Dtos.Quizzes.GenerateQuiz
{
    public class GenerateQuestionDto
    {
        public required string Title { get; set; }
        public required List<GenerateAnswerDto> GenerateAnswersDto { get; set; }
    }
}
