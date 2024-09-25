using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuizParticipationRepository
{
    Task Add(QuizParticipation quizParticipation);
    Task<QuizParticipation?> GetQuizParticipation(Guid id);
    Task<QuizParticipation?> GetByIdWithUserAnswers(Guid quizParticipationId);
    Task<List<QuizParticipation>> GetByParticipantId(string participantId);
    Task Update(QuizParticipation quizParticipation);
    Task<(List<QuizParticipation> quizparticipations, int totalCount)> GetQuizParticipationsForQuiz(Guid quizId, int pageSize, int pageNumber);
}