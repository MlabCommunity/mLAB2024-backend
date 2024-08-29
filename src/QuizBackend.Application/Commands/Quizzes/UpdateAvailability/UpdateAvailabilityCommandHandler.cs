using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Commands.Quizzes.UpdateStatusQuiz;
using QuizBackend.Application.Commands.UpdateStatusQuiz;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.Quizzes.UpdateAvailability
{
    public record UpdateAvailabilityResponse(Guid Id, Availability NewAvailability);
    public class UpdateAvailabilityCommandHandler : ICommandHandler<UpdateAvailabilityCommand, UpdateAvailabilityResponse>
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateAvailabilityCommandHandler(IQuizRepository quizRepository, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
        {
            _quizRepository = quizRepository;
            _dateTimeProvider = dateTimeProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateAvailabilityResponse> Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();
            var quiz = await _quizRepository.GetByIdAndOwnerAsync(request.Id, userId, cancellationToken)
                ?? throw new BadRequestException($"Quiz with ID {request.Id} not found for the current user.");

            var updatedAtUtc = _dateTimeProvider.UtcNow;
            quiz.Availability = request.Availability;
            quiz.UpdatedAtUtc = updatedAtUtc;

            await _quizRepository.UpdateAsync(quiz, cancellationToken);

            return new UpdateAvailabilityResponse(quiz.Id, quiz.Availability);
        }
    }
}
