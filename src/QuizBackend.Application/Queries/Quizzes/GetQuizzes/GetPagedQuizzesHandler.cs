using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizzes
{
    public class GetPagedQuizzesHandler : IQueryHandler<GetPagedQuizzesQuery, PagedDto<QuizDto>>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPagedQuizzesHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
        {
            _quizRepository = quizRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedDto<QuizDto>> Handle(GetPagedQuizzesQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page ?? 1;
            var pageSize = request.PageSize ?? 10;
            var userId = _httpContextAccessor.GetUserId();

            var (quizzes, totalCount) = await _quizRepository.Get(userId, pageSize, page, cancellationToken);

            var quizzesDtos = quizzes.Select(quiz => new QuizDto(
                     quiz.Id,
                     quiz.Title,
                     quiz.Description,
                     quiz.Availability,
                     quiz.Status,
                     quiz.Questions.Count
                 )).ToList();

            return new PagedDto<QuizDto>(quizzesDtos, totalCount, pageSize, page);
        }
    }
}
