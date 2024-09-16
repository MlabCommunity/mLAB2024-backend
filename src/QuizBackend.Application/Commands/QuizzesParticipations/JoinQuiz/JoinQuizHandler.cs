using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using QuizBackend.Application.Extensions;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.QuizzesParticipations.JoinQuiz;

public record JoinCodeRequest(string JoinCode);
public record JoinQuizResponse(Guid Id);

public class JoinQuizHandler : ICommandHandler<JoinQuizCommand, JoinQuizResponse>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizParticipationRepository _participationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAuthService _authService;

    public JoinQuizHandler(IQuizRepository quizRepository, IQuizParticipationRepository participationRepository, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider, IAuthService authService)
    {
        _quizRepository = quizRepository;
        _participationRepository = participationRepository;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
        _authService = authService;
    }

    public async Task<JoinQuizResponse> Handle(JoinQuizCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var quiz = await _quizRepository.GetQuizByJoinCode(request.JoinCode)
            ?? throw new NotFoundException(nameof(Quiz), request.JoinCode);

        if (quiz.Status == Status.Inactive)
        {
            throw new BadRequestException($"Quiz Status is Inactive. You cannot join to quiz - {quiz.JoinCode}");
        }

        var quizParticipation = CreateQuizParticipation(quiz.Id, userId);
        await _participationRepository.Add(quizParticipation);

        return new JoinQuizResponse(quizParticipation.Id);
    }

    private QuizParticipation CreateQuizParticipation(Guid quizId, string userId)
    {
        return new QuizParticipation
        {
            Id = Guid.NewGuid(),
            QuizId = quizId,
            ParticipantId = userId,
            ParticipationDateUtc = _dateTimeProvider.UtcNow,
            CreatedAtUtc = _dateTimeProvider.UtcNow
        };
    }
}