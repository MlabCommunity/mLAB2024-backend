using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.GenerateQuiz
{
    public record GenerateQuizCommand(
        string Content,
        int NumberOfQuestions,
        QuestionType QuestionType
    ) : ICommand<CreateQuizDto>;
}
