using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuizResultRepository
{
    Task Add(QuizResult quizResult);
    Task<QuizResult> GetByQuizParticipationId(Guid quizParticipationId);
    Task Update(QuizResult quizResult);
}