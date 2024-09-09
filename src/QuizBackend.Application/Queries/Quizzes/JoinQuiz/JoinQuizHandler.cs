using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.JoinQuiz;

public record JoinQuizAnswersResponse(Guid Id, string Content);
public record JoinQuizQuestionsResponse(Guid Id, string Title, List<JoinQuizAnswersResponse> Answers);
public record JoinQuizResponse(Guid Id, string Title, string? Description, List<JoinQuizQuestionsResponse> Questions);
public class JoinQuizHandler : ICommandHandler<JoinQuizCommand, JoinQuizResponse>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IQuizParticipationRepository _participationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JoinQuizHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider, IQuizParticipationRepository quizParticipationRepository)
    {
        _quizRepository = quizRepository;
        _participationRepository = quizParticipationRepository;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<JoinQuizResponse> Handle(JoinQuizCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var quiz = await _quizRepository.GetQuizByJoinCode(request.JoinCode)
            ?? throw new NotFoundException(nameof(Quiz), request.JoinCode);

        var questions = quiz.Questions.Select(question => new JoinQuizQuestionsResponse(
           question.Id,
           question.Title,
           question.Answers.Select(answer => new JoinQuizAnswersResponse(
               answer.Id,
               answer.Content
           )).ToList())).ToList();

        var quizParticipation = new QuizParticipation
        {
            Id = Guid.NewGuid(),
            QuizId = quiz.Id,
            ParticipantId = userId,
            ParticipationDateUtc = _dateTimeProvider.UtcNow,
            CreatedAtUtc = _dateTimeProvider.UtcNow
        };

        await _participationRepository.Add(quizParticipation);
        
        var response = new JoinQuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            questions);

        return response;
    }
}
