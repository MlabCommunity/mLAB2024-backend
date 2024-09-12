using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Application.Interfaces.Users;
using QuizBackend.Application.Interfaces;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using QuizBackend.Application.Extensions;

namespace QuizBackend.Application.Commands.Quizzes.JoinQuiz;

public record JoinQuizResponse(Guid Id, string RefreshToken, string? AccessToken);
public record UserAuthResponse(string UserId, string RefreshToken, string? AccessToken);

public class JoinQuizHandler : ICommandHandler<JoinQuizCommand, JoinQuizResponse>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizParticipationRepository _participationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public JoinQuizHandler(IQuizRepository quizRepository, IQuizParticipationRepository participationRepository, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider, IAuthService authService, IJwtService jwtService)
    {
        _quizRepository = quizRepository;
        _participationRepository = participationRepository;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
        _authService = authService;
        _jwtService = jwtService;
    }

    public async Task<JoinQuizResponse> Handle(JoinQuizCommand request, CancellationToken cancellationToken)
    {
        var userAuth = await GetCurrentUserOrCreateGuest(request.UserName);

        var quiz = await _quizRepository.GetQuizByJoinCode(request.JoinCode)
            ?? throw new NotFoundException(nameof(Quiz), request.JoinCode);

        var quizParticipation = CreateQuizParticipation(quiz.Id, userAuth.UserId);
        await _participationRepository.Add(quizParticipation);

        return new JoinQuizResponse(quizParticipation.Id, userAuth.RefreshToken, userAuth.AccessToken);
    }

    private async Task<UserAuthResponse> GetCurrentUserOrCreateGuest(string? displayName)
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = _httpContextAccessor.GetUserId();
            var token = await _jwtService.GenerateOrRetrieveRefreshTokenAsync(userId);

            return new UserAuthResponse(_httpContextAccessor.GetUserId(), token, token);
        }

        var guest = await _authService.CreateGuestUser(displayName!);
        var authResult = await _authService.LoginGuest(guest);
        return new UserAuthResponse(guest.Id, authResult.RefreshToken, authResult.AccessToken);
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