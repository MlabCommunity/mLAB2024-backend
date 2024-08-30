using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;

namespace QuizBackend.Application.Interfaces
{
    public interface IQuizService
    {
        Task<CreateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command);
    }
}
