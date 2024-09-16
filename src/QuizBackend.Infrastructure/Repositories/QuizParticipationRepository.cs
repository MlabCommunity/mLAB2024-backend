using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories;

public class QuizParticipationRepository : IQuizParticipationRepository
{
    private readonly AppDbContext _dbContext;

    public QuizParticipationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QuizParticipation> GetById(Guid quizParticipationId)
    {
        return await _dbContext.QuizParticipations
            .Include(qp => qp.Quiz)
            .ThenInclude(q => q.Questions)
            .Include(qp => qp.UserAnswers)
            .FirstOrDefaultAsync(qp => qp.Id == quizParticipationId);
    }

    public async Task Update(QuizParticipation quizParticipation)
    {
        _dbContext.QuizParticipations.Update(quizParticipation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<QuizParticipation>> GetByParticipantId(string participantId)
    {
        return await _dbContext.QuizParticipations
            .Include(q => q.Quiz)
            .Include(q => q.UserAnswers)
            .Include(q => q.QuizResult)
            .Where(q => q.ParticipantId == participantId)
            .ToListAsync();
    }
}