using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.UpdateQuestionAndAnswers;

public class UpdateQuestionAndAnswersCommandHandler : ICommandHandler<UpdateQuestionAndAnswersCommand, Unit>
{
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(UpdateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        var questionEntity = await _questionAndAnswersRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Question), request.Id.ToString());

        if (questionEntity.Quiz.OwnerId != userId)
            throw new BadRequestException("Question not found");

        request.UpdateEntity(questionEntity,_dateTimeProvider);
        await _questionAndAnswersRepository.Update(questionEntity);

        return Unit.Value;
    }
}