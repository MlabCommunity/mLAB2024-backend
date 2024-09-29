using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _dbContext;
    public QuizRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Quiz?> GetQuizForUser(Guid quizId, string userId)
    {
        return await _dbContext.Quizzes
            .AsNoTracking()
            .Include(quiz => quiz.Owner)
            .Include(quiz => quiz.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(x => x.Id == quizId && x.Owner.Id == userId);
    }

    public async Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var quiz = await _dbContext.Quizzes
            .AsNoTracking()
            .Include(quiz => quiz.Owner)
            .Include(quiz => quiz.Questions)
            .ThenInclude(q => q.Answers)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return quiz;
    }

    public async Task<(List<Quiz> quizzes, int totalCount)> Get(string userId, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        var baseQuery = _dbContext.Quizzes
            .AsNoTracking()
            .Where(q => q.OwnerId == userId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var quizzes = await baseQuery
            .Include(quiz => quiz.Questions)
            .OrderByDescending(quiz => quiz.CreatedAtUtc)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (quizzes, totalCount);
    }

    public async Task AddAsync(Quiz quiz)
    {
        _dbContext.Quizzes.Add(quiz);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Quiz?> GetByIdAndOwnerAsync(Guid id, string ownerId, CancellationToken cancellationToken)
    {
        return await _dbContext.Quizzes
            .FirstOrDefaultAsync(q => q.Id == id && q.OwnerId == ownerId, cancellationToken);
    }

    public async Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken)
    {
        _dbContext.Quizzes.Update(quiz);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Quiz quiz, CancellationToken cancellationToken)
    {
        _dbContext.Quizzes.Remove(quiz);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistsWithCode(string code)
    {
        return await _dbContext.Quizzes.AnyAsync(q => q.JoinCode == code);
    }

    public async Task<Quiz?> GetQuizByJoinCode(string code)
    {
        return await _dbContext.Quizzes
            .AsNoTracking()
            .Include(q => q.Questions)
            .ThenInclude(quest => quest.Answers)
            .FirstOrDefaultAsync(q => q.JoinCode == code);
    }

    public async Task UpdateQuizzesStatusForUser(string userId, Status status)
    {
        await _dbContext.Quizzes
            .IgnoreQueryFilters()
            .Where(q => q.OwnerId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(q => q.Status, status));
    }
}