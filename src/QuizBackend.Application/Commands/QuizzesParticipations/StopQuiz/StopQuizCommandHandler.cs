using MediatR;
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

    public StopQuizCommandHandler(IQuizParticipationRepository quizParticipationRepository, IDateTimeProvider dateTimeProvider)
    {
        _quizParticipationRepository = quizParticipationRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(StopQuizCommand request, CancellationToken cancellationToken)
    {
        var quizParticipation = await _quizParticipationRepository.GetById(request.QuizParticipationId)
            ?? throw new NotFoundException(nameof(QuizParticipation), request.QuizParticipationId.ToString());

        if (quizParticipation.Status == QuizParticipationStatus.Stopped)
        {
            throw new ApplicationException("Quiz has already been stopped");
        }

        quizParticipation.Status = QuizParticipationStatus.Stopped;
        quizParticipation.CompletionTime = _dateTimeProvider.UtcNow;

        await _quizParticipationRepository.Update(quizParticipation);

        return Unit.Value;
    }
}