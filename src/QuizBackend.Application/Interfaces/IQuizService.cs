using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;

namespace QuizBackend.Application.Interfaces
{
    public interface IQuizService
    {
        Task<GenerateQuizResponse> GenerateQuizFromPromptTemplateAsync(GenerateQuizCommand command);
    }
}
