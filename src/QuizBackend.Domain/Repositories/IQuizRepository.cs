using QuizBackend.Domain.Entities;
using System.Linq.Expressions;

namespace QuizBackend.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default);
        Task<Quiz?> GetQuizForUser(Guid quizId, string userId);
    }
}
