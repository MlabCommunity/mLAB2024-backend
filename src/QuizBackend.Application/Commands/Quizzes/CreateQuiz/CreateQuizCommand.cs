using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public record CreateQuizCommand(
        string Title,
        string Description,
        QuestionType QuestionType,
        List<CreateQuestion> CreateQuestions
        ) : ICommand<Guid>;
    
    public record CreateQuestion(
        string Title,
        List<CreateAnswer> CreateAnswers);
    public record CreateAnswer(
        string Content,
        bool IsCorrect);
}