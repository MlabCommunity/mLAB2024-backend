using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuestionAndAnswersRepository
{
    Task Add(Question question);
    Task Delete(Guid Id);
    Task<Question?> GetById(Guid Id);
    Task Update(Question question);
    Task<Answer> GetCorrectAnswerByQuestionId(Guid questionId);
    Task<List<Answer>> GetAnswersByQuestionId(Guid questionId);
    Task<List<Question>> GetQuestionsByQuizId(Guid quizId);
    Task<Quiz?> GetQuizForQuestion(Guid questionId);
}