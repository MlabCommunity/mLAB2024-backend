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

    public async Task Add(QuizParticipation quizParticipation)
    {
       _dbContext.Add(quizParticipation);
       await _dbContext.SaveChangesAsync();
    }
}
