using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuizParticipationRepository
{
    Task Add(QuizParticipation quizParticipation);
    Task<QuizParticipation?> GetQuizParticipation(Guid id);
    Task<List<QuizParticipation>> GetByParticipantId(string participantId);
    Task Update(QuizParticipation quizParticipation);
}