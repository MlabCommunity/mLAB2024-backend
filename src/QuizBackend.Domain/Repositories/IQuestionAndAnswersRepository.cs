using QuizBackend.Domain.Entities;

namespace QuizBackend.Domain.Repositories;

public interface IQuestionAndAnswersRepository
{
    Task Add(Question question);
    Task Delete(Guid Id);
    Task<Question?> GetById(Guid Id);
    Task Update(Question question);
}