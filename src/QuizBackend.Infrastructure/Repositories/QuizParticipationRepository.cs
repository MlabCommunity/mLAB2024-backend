﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<QuizParticipation?> GetQuizParticipation(Guid id)
    {
        return await _dbContext
           .QuizParticipations
           .AsNoTracking()
           .Include(q => q.Quiz)
           .ThenInclude(q => q.Questions)
           .ThenInclude(q => q.Answers)
           .FirstOrDefaultAsync(q => q.Id == id);
    }
    public async Task<QuizParticipation?> GetByIdWithUserAnswers(Guid quizParticipationId)
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
            .AsNoTracking()
            .Include(q => q.Quiz)
            .Include(q => q.UserAnswers)
            .Include(q => q.QuizResult)
            .Where(q => q.ParticipantId == participantId)
            .ToListAsync();
    }

    public async Task<(List<QuizParticipation> quizparticipations, int totalCount)> GetQuizParticipationsForQuiz(Guid quizId, int pageSize, int pageNumber)
    {
        var baseQuery = _dbContext.QuizParticipations
            .AsNoTracking()
            .Where(qp => qp.QuizId == quizId)
            .Include(qp => qp.Participant)
            .Include(qp => qp.QuizResult);
         
        var totalCount = await baseQuery.CountAsync();

        var quizParticipations = await baseQuery
            .OrderByDescending(qp => qp.ParticipationDateUtc)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (quizParticipations, totalCount);
    }
}