using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public record CreateQuizCommand(Guid Id, string Title, string Description, string OwnerId, List<CreateQuestion> Questions) : ICommand<Guid>;
    public record CreateQuestion(Guid Id, string Title, List<CreateAnswer> Answers, Guid QuizId);
    public record CreateAnswer(Guid Id, string Content, bool IsCorrect, Guid QuestionId);
}