using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizzes
{
    public class GetPagedQuizzesHandler : IQueryHandler<GetPagedQuizzesQuery, PagedDto<QuizDto>>
    {
        private readonly IQuizRepository _quizRepository;
       
        public GetPagedQuizzesHandler(IQuizRepository quizRepository, IUserContext userContext)
        {
            _quizRepository = quizRepository;
        }

        public async Task<PagedDto<QuizDto>> Handle(GetPagedQuizzesQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page ?? 1;
            var pageSize = request.PageSize ?? 10;

            var (quizzes, totalCount) = await _quizRepository.Get(pageSize, page);

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
