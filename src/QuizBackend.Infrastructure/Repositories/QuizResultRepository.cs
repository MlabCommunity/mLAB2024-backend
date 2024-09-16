using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories;

public class QuizResultRepository : IQuizResultRepository
{
    private readonly AppDbContext _dbContext;

    public QuizResultRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(QuizResult quizResult)
    {
        _dbContext.QuizResults.Add(quizResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(QuizResult quizResult)
    {
        _dbContext.QuizResults.Update(quizResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<QuizResult> GetByQuizParticipationId(Guid quizParticipationId)
    {
        return await _dbContext.QuizResults
            .FirstOrDefaultAsync(qr => qr.QuizParticipationId == quizParticipationId);
    }
}