using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuizParticipationRepository
{
    Task<QuizParticipation> GetById(Guid quizParticipationId);
    Task Update(QuizParticipation quizParticipation);
    Task<List<QuizParticipation>> GetByParticipantId(string participantId);
}