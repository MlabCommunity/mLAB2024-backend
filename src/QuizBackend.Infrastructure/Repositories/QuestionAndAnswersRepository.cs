using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Repositories;

public class QuestionAndAnswersRepository : IQuestionAndAnswersRepository
{
    private readonly AppDbContext _dbContext;
    public QuestionAndAnswersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Question?> GetById(Guid id)
    {
        return await _dbContext.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task Add(Question question)
    {
        _dbContext.Questions.Add(question);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Question question)
    {
        _dbContext.Questions.Update(question);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await _dbContext.Questions.FindAsync(id);
        if (entity != null)
        {
            _dbContext.Questions.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}