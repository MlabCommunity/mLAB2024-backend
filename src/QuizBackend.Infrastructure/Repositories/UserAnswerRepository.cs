using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories;

public class UserAnswerRepository : IUserAnswerRepository
{
    private readonly AppDbContext _dbContext;
    public UserAnswerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(UserAnswer userAnswer)
    {
        _dbContext.UserAnswers.Add(userAnswer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserAnswer> GetById(Guid id)
    {
        return await _dbContext.UserAnswers.FindAsync(id);
    }

    public async Task<List<UserAnswer>> GetByQuizParticipationId(Guid quizParticipationId)
    {
        return await _dbContext.UserAnswers
            .Where(ua => ua.QuizParticipationId == quizParticipationId)
            .ToListAsync();
    }
}