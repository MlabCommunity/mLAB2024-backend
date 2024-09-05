using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizzes;

public record GetPagedQuizzesQuery(int? Page, int? PageSize) : IQuery<PagedDto<QuizDto>>;