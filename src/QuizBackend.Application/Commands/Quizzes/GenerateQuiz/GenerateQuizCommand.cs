using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public class GenerateQuizCommand : ICommand<GenerateQuizDto>
    {
        public string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public QuestionType QuestionType { get; set; }

        public GenerateQuizCommand(string content, int numberOfQuestions, QuestionType questionType)
        {
            Content = content;
            NumberOfQuestions = numberOfQuestions;
            QuestionType = questionType;
        }
    }
}
