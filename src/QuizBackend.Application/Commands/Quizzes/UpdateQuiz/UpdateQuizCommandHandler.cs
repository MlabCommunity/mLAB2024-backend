﻿using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.Quizzes;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.Quizzes.UpdateQuiz;

public class UpdateQuizCommandHandler : ICommandHandler<UpdateQuizCommand, Unit>
{
    private readonly IQuizRepository _quizRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateQuizCommandHandler(IQuizRepository quizRepository, IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider)
    {
        _quizRepository = quizRepository;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        var quizEntity = await _quizRepository.GetByIdAndOwnerAsync(request.Id, userId, cancellationToken)
             ?? throw new NotFoundException(nameof(Quiz), request.Id.ToString());

        request.UpdateEntity(quizEntity, _dateTimeProvider);

        await _quizRepository.UpdateAsync(quizEntity, cancellationToken);

        return Unit.Value;
    }
}