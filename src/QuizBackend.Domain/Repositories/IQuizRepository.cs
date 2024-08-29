using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default);

        Task<(List<Quiz> quizzes, int totalCount)> Get(int pageSize, int pageNumber, CancellationToken cancellationToken = default);

        Task AddAsync(Quiz quiz);

        Task UpdateStatusAsync(Quiz quiz, CancellationToken cancellationToken);

        Task<Quiz?> GetByIdAndOwnerAsync(Guid id, string ownerId, CancellationToken cancellationToken);

    }
}