using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.Quizzes.GetQuiz;

public record GetQuizQuery(Guid Id, int? Page, int? PageSize) : IQuery<PagedDto<QuizDetailsDto>>;