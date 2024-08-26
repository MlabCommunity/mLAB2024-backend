using QuizBackend.Application.Commands.GenerateQuiz;
using QuizBackend.Application.Dtos.CreateQuiz;
using QuizBackend.Application.Dtos.Quiz;

namespace QuizBackend.Application.Interfaces
{
    public interface IQuizService
    {
        Task<CreateQuizDto> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command);
    }
}
