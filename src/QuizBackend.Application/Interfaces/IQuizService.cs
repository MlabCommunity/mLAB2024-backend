using QuizBackend.Application.Commands.Quizzes.GenerateQuiz;
using QuizBackend.Application.Dtos.Quizzes;

namespace QuizBackend.Application.Interfaces;

public interface IQuizService
{
    Task<GenerateQuizResponse> GenerateQuizFromPromptTemplate(GenerateQuizCommand command);
    Task<GenerateQuizResponse> RegenerateQuizFromPromptTemplate();
}