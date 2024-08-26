using Microsoft.EntityFrameworkCore;
using QuizBackend.Application.Dtos.Quizzes.CreateQuiz;
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

        public async Task AddAsync(Quiz quiz)
        {
            _dbContext.Quizzes.Add(quiz);
            await _dbContext.SaveChangesAsync();
        }
    }
}
