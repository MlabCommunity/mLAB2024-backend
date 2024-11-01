﻿using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuiz;

public class GetQuizQueryHandler : IQueryHandler<GetQuizQuery, QuizDetailsDto>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private const int MaxPageSize = 10;

    public GetQuizQueryHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor, IQuizParticipationRepository quizParticipationRepository)
    {
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
        _quizParticipationRepository = quizParticipationRepository;
    }

    public async Task<QuizDetailsDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        if (request.Page < 1 ||
            (request.PageSize.HasValue && (request.PageSize <= 0 || request.PageSize > MaxPageSize)))
        {
            throw new BadRequestException("Page number must be greater than 0 and page size must be greater than 0 and less than or equal to 10.");
        }
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;
        var quiz = await _quizRepository.GetQuizForUser(request.Id, _httpContextAccessor.GetUserId())
                   ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

        var httpRequest = _httpContextAccessor.HttpContext?.Request;
        var shareLink = $"{httpRequest!.Scheme}://{httpRequest.Host}/{quiz.JoinCode}";
        var (quizParticipations, totalCount) = await _quizParticipationRepository.GetQuizParticipationsForQuiz(quiz.Id, pageSize, page);

        return quiz.ToResponse(shareLink, (quizParticipations, totalCount, pageSize, page));
    }
}