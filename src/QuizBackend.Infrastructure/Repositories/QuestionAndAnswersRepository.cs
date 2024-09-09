using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Common;
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

    public async Task<PagedEntity<Question>> GetPagedQuestionsForQuiz(Guid quizId, int pageNumber, int pageSize)
    {
        var totalItems = await _dbContext.Questions.CountAsync(q => q.QuizId == quizId);
        var questions = await _dbContext.Questions
            .AsNoTracking()
            .Include(q => q.Answers)
            .Where(q => q.QuizId == quizId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedEntity<Question>(questions, totalItems);
    }
}