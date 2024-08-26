using QuizBackend.Application.Dtos.Quiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.GenerateQuiz
{
    public class GenerateQuizCommand : ICommand<CreateQuizDto>
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
