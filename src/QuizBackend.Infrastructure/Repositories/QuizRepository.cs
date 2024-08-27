using Microsoft.EntityFrameworkCore;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserContext _userContext;
        public QuizRepository(AppDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
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

        public async Task<(List<Quiz> quizzes, int totalCount)> Get(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            var userId = _userContext.UserId;

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
    }
}
