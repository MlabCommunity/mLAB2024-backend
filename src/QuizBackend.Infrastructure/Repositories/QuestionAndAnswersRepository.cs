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
            .AsNoTracking()
            .Include(q => q.Quiz)
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

    public async Task Delete(Question question)
    {
       _dbContext.Questions.Remove(question);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<Answer> GetCorrectAnswerByQuestionId(Guid questionId)
    {
        return await _dbContext.Answers
            .FirstOrDefaultAsync(a => a.QuestionId == questionId && a.IsCorrect);
    }

    public async Task<List<Answer>> GetAnswersByQuestionId(Guid questionId)
    {
        return await _dbContext.Answers
            .AsNoTracking()
            .Where(a => a.QuestionId == questionId)
            .ToListAsync();
    }

    public async Task<List<Question>> GetQuestionsByQuizId(Guid quizId)
    {
        return await _dbContext.Questions
            .AsNoTracking()
            .Include(q => q.Answers)
            .Where(q => q.QuizId == quizId)
            .ToListAsync();
    }
}