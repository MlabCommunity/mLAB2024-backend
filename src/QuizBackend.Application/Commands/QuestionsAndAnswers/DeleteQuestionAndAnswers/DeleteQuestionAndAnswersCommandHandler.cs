using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.DeleteQuestionAndAnswers;

public class DeleteQuestionAndAnswersCommandHandler : ICommandHandler<DeleteQuestionAndAnswersCommand, Unit>
{
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository, IHttpContextAccessor httpContextAccessor)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<Unit> Handle(DeleteQuestionAndAnswersCommand request, CancellationToken cancellation)
    {
        var questionEntity = await _questionAndAnswersRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Question), request.Id.ToString());

        if (questionEntity.Quiz.OwnerId != _httpContextAccessor.GetUserId())
            throw new ForbidException(
               "You do not have permission to delete this resource",
               resourceName: nameof(Question),
               actionAttempted: "Delete");

        await _questionAndAnswersRepository.Delete(questionEntity);

        return Unit.Value;
    }
}