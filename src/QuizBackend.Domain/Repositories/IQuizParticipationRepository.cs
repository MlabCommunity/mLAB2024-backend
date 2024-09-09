using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;
public interface IQuizParticipationRepository
{
    Task Add(QuizParticipation quizParticipation);
}
