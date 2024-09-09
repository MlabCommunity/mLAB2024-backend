using QuizBackend.Domain.Common;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories
{
    public interface IQuestionAndAnswersRepository
    {
        Task Add(Question question);
        Task Delete(Guid Id);
        Task<Question?> GetById(Guid Id);
        Task Update(Question question);
        Task<PagedEntity<Question>> GetPagedQuestionsForQuiz(Guid quizId, int pageNumber, int pageSize);
    }
}