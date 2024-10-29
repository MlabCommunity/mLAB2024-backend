using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IUserAnswerRepository
{
    Task Add(UserAnswer userAnswer);
    Task<UserAnswer> GetById(Guid id);
    Task<List<UserAnswer>> GetByQuizParticipationId(Guid quizParticipationId);
    Task AddRange(List<UserAnswer> userAnswers);
}