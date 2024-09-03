using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public record GenerateQuizCommand(
        string Content,
        int NumberOfQuestions,
        List<QuestionType> QuestionTypes
    ) : ICommand<CreateQuizDto>;
}
