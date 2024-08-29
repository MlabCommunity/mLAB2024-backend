using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.GenerateQuiz;

namespace QuizBackend.Application.Interfaces
{
    public interface IQuizService
    {
        Task<GenerateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command);
    }
}
