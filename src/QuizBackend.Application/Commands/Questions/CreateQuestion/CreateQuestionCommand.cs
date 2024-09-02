using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.CreateQuestion
{
    public record CreateQuestionCommand(string Title, Guid QuizId, List<AnswerDto> Answers) : ICommand<Guid>;
}
