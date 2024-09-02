using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public record CreateQuizResponse(Guid Id);
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, Guid>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateQuizCommandHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
        {
            _quizRepository = quizRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _httpContextAccessor.GetUserId();

            var quiz = request.ToEntity(ownerId);

            await _quizRepository.AddAsync(quiz);

            return quiz.Id;
        }
    }
}
