using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public class CreateQuizCommand : ICommand<Guid>
    {
        public CreateQuizDto QuizDto { get; }

        public CreateQuizCommand(CreateQuizDto quizDto)
        {
            QuizDto = quizDto;
        }
    }
}