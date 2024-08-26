using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _dbContext;

        public QuizRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Quiz?> Get(Guid id, CancellationToken cancellationToken = default)
        {
            Quiz? quiz = await _dbContext.Quizzes
                .AsNoTracking()
                .Include(quiz => quiz.Owner)
                .Include(quiz => quiz.Questions)
                .ThenInclude(q => q.Answers)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return quiz;
        }
    }
}
