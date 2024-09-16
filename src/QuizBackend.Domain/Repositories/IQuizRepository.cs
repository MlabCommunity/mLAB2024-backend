using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuizRepository
{
    Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default);

    Task<(List<Quiz> quizzes, int totalCount)> Get(string userId, int pageSize, int pageNumber, CancellationToken cancellationToken = default);

    Task AddAsync(Quiz quiz);

    Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken);

    Task<Quiz?> GetByIdAndOwnerAsync(Guid id, string ownerId, CancellationToken cancellationToken);

    Task<Quiz?> GetQuizForUser(Guid quizId, string userId);

    Task RemoveAsync(Quiz quiz, CancellationToken cancellationToken);

    Task<bool> IsExistsWithCode(string code);

    Task<Quiz?> GetQuizByJoinCode(string code);
}