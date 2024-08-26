using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using System.Security.Claims;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz
{
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, Guid>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (ownerId == null)
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            var quizEntity = request.QuizDto.ToEntity(ownerId);
            await _quizRepository.AddAsync(quizEntity);

            return quizEntity.Id;
        }
    }
}
