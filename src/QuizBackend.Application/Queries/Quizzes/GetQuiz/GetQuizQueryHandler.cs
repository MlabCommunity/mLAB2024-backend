using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.Quizzes.GetQuiz;

public class GetQuizQueryHandler : IQueryHandler<GetQuizQuery, QuizDetailsDto>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetQuizQueryHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor)
    {
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<QuizDetailsDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetQuizForUser(request.Id, _httpContextAccessor.GetUserId())
                   ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

        var questionsDto = quiz.Questions
            .Select(q => new QuestionDto(
                q.Id,
                 q.Title,
                 q.Answers.Select(a => new AnswerDto(
                     a.Id,
                     a.Content,
                     a.IsCorrect
                 )).ToList()
             ))
             .ToList();

        var httpRequest = _httpContextAccessor.HttpContext?.Request;
        var shareLink = $"{httpRequest!.Scheme}://{httpRequest.Host}/{quiz.JoinCode}";

        var quizDto = new QuizDetailsDto(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            shareLink,
            quiz.Availability,
            quiz.Status,
            questionsDto);

        return quizDto;
    }
}