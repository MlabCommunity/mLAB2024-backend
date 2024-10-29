using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuestionAndAnswersRepository
{
    Task Add(Question question);
    Task Delete(Question question);
    Task<Question?> GetById(Guid Id);
    Task Update(Question question);
    Task<Answer> GetCorrectAnswerByQuestionId(Guid questionId);
    Task<List<Answer>> GetAnswersByQuestionId(Guid questionId);
    Task<List<Question>> GetQuestionsByQuizId(Guid quizId);
    Task<bool> IsQuestionsAndAnswersExist(Guid quizId, List<Guid> questionsIds, List<Guid> answersIds);
}