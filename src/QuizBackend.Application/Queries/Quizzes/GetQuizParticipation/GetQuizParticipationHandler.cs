using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizParticipation;

public record QuizResponse(Guid Id, string Title, string? Description, List<QuizQuestionsResponse> Questions);
public record QuizAnswersResponse(Guid Id, string Content);
public record QuizQuestionsResponse(Guid Id, string Title, List<QuizAnswersResponse> Answers);
public record QuizParticipationResponse(Guid Id, string ParticipationDate, QuizResponse QuizResponse);

public class GetQuizParticipationHandler : IQueryHandler<GetQuizParticipationQuery, QuizParticipationResponse>
{
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetQuizParticipationHandler(IQuizParticipationRepository quizParticipationRepository, IHttpContextAccessor httpContextAccessor)
    {
        _quizParticipationRepository = quizParticipationRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<QuizParticipationResponse> Handle(GetQuizParticipationQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var quizParticipation = await _quizParticipationRepository.GetQuizParticipation(request.Id)
            ?? throw new NotFoundException(nameof(QuizParticipation), request.Id.ToString());

        var quiz = quizParticipation.Quiz;

        var questions = quiz.Questions.Select(question => new QuizQuestionsResponse(
          question.Id,
          question.Title,
          question.Answers.Select(answer => new QuizAnswersResponse(
              answer.Id,
              answer.Content
          )).ToList())).ToList();

        var participationDate = quizParticipation.ParticipationDateUtc.ToString("dd.MM.yyyy HH:mm");
        var quizResponse = new QuizResponse(quiz.Id, quiz.Title, quiz.Description, questions);
        return new QuizParticipationResponse(quizParticipation.Id, participationDate, quizResponse);
    }
}