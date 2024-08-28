using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Quiz quiz);
    }
}