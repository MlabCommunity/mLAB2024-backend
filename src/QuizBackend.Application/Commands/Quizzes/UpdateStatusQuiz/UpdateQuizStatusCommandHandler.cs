using MediatR;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.UpdateStatusQuiz
{
    public record UpdateQuizStatusResponse(Guid QuizId, Status NewStatus);
    public class UpdateQuizStatusCommandHandler : ICommandHandler<UpdateStatusQuizCommand, UpdateQuizStatusResponse>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateQuizStatusCommandHandler(
            IQuizRepository quizRepository,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider)
        {
            _quizRepository = quizRepository;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<UpdateQuizStatusResponse> Handle(UpdateStatusQuizCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var quiz = await _quizRepository.GetByIdAndOwnerAsync(request.Id, userId, cancellationToken)
                ?? throw new BadRequestException($"Quiz with ID {request.Id} not found for the current user.");

            var updatedAtUtc = _dateTimeProvider.UtcNow;
            quiz.Status = request.Status;
            quiz.UpdatedAtUtc = updatedAtUtc;

            await _quizRepository.UpdateStatusAsync(quiz, cancellationToken);

            return new UpdateQuizStatusResponse(quiz.Id, quiz.Status);
        }
    }
}
