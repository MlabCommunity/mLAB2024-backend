using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.CreateQuestion
{
    public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : ICommand<Guid>;
}
