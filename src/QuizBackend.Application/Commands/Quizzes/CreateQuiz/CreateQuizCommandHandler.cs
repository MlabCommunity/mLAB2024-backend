using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.Quizzes;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace QuizBackend.Application.Commands.Quizzes.CreateQuiz;

public record CreateQuizResponse(Guid Id, string Url);

public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, CreateQuizResponse>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateQuizCommandHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider)
    {
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<CreateQuizResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var ownerId = _httpContextAccessor.GetUserId();

        var httpRequest = _httpContextAccessor.HttpContext?.Request;

        string joinCode = GenerateJoinCode();
   
        var quiz = request.ToEntity(ownerId, joinCode, _dateTimeProvider);
        await _quizRepository.AddAsync(quiz);

        var url = $"{httpRequest!.Scheme}://{httpRequest.Host}/{joinCode}";
        return new CreateQuizResponse(quiz.Id, url);
    }

    private static string GenerateJoinCode(int length = 8)
    {
        var guid = Guid.NewGuid().ToString();
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(guid));
        var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        return hash.Substring(0, length);
    }
}