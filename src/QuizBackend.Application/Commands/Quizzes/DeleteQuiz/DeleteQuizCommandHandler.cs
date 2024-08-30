using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.Quizzes.DeleteQuiz
{
    public class DeleteQuizCommandHandler : ICommandHandler<DeleteQuizCommand, Unit>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteQuizCommandHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
        {
            _quizRepository = quizRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            
            var userId = _httpContextAccessor.GetUserId();

            var quiz = await _quizRepository.GetByIdAndOwnerAsync(request.Id, userId, cancellationToken)
                         ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

            await _quizRepository.RemoveAsync(quiz, cancellationToken);

            return Unit.Value;
        }
    }
}
