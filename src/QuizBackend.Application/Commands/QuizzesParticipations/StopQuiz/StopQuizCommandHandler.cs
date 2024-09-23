using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuizzesParticipations.StopQuiz;

public class StopQuizCommandHandler : ICommandHandler<StopQuizCommand, Unit>
{
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StopQuizCommandHandler(IQuizParticipationRepository quizParticipationRepository, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
    {
        _quizParticipationRepository = quizParticipationRepository;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(StopQuizCommand request, CancellationToken cancellationToken)
    {
        var quizParticipation = await _quizParticipationRepository.GetQuizParticipation(request.QuizParticipationId)
            ?? throw new NotFoundException(nameof(QuizParticipation), request.QuizParticipationId.ToString());

        if (quizParticipation.ParticipantId != _httpContextAccessor.GetUserId())
        {
            throw new BadRequestException("Quiz not found");
        }

        if (quizParticipation.Status == QuizParticipationStatus.Stopped)
        {
            throw new BadRequestException("Quiz has already been stopped");
        }

        quizParticipation.Status = QuizParticipationStatus.Stopped;
        quizParticipation.CompletionTime = _dateTimeProvider.UtcNow;

        await _quizParticipationRepository.Update(quizParticipation);

        return Unit.Value;
    }
}