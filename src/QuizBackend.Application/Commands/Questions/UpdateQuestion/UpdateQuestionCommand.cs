using QuizBackend.Application.Dtos.Quizzes.UpdateQuiz;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Questions.UpdateQuestion
{
    public record UpdateQuestionCommand(UpdateQuestionDto QuestionDto) : ICommand<Guid>;
}
