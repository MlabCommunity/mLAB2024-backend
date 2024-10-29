using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Commands.UpdateStatusQuiz;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.Quizzes.UpdateStatusQuiz;

public record UpdateQuizStatusResponse(Guid QuizId, Status NewStatus);
public class UpdateQuizStatusCommandHandler : ICommandHandler<UpdateStatusQuizCommand, UpdateQuizStatusResponse>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateQuizStatusCommandHandler(
        IQuizRepository quizRepository,
        IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor httpContextAccessor)
    {
        _quizRepository = quizRepository;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateQuizStatusResponse> Handle(UpdateStatusQuizCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var quiz = await _quizRepository.GetByIdAndOwnerAsync(request.Id, userId, cancellationToken)
            ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

        var updatedAtUtc = _dateTimeProvider.UtcNow;
        quiz.Status = request.Status;
        quiz.UpdatedAtUtc = updatedAtUtc;

        await _quizRepository.UpdateAsync(quiz, cancellationToken);

        return new UpdateQuizStatusResponse(quiz.Id, quiz.Status);
    }
}