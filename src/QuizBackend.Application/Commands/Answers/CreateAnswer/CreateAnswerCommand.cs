using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces.Messaging;


namespace QuizBackend.Application.Commands.Answers.CreateAnswer
{
    public record CreateAnswerCommand(CreateAnswerDto AnswerDto) : ICommand<Guid>;
}
