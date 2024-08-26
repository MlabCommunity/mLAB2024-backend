using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public class GenerateQuizCommand : ICommand<GenerateQuizDto>
    {
        public string Content { get; set; }
        public int NumberOfQuestions { get; set; }
        public string TypeOfQuestions { get; set; }

        public GenerateQuizCommand(string content, int numberOfQuestions, string typesOfQuestions)
        {
            Content = content;
            NumberOfQuestions = numberOfQuestions;
            TypeOfQuestions = typesOfQuestions;
        }
    }
}
