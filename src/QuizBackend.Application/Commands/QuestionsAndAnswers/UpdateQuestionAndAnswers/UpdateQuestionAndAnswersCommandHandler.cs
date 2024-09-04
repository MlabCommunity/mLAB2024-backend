using MediatR;
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

    public UpdateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository, IDateTimeProvider dateTimeProvider)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(UpdateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
    {
        var questionEntity = await _questionAndAnswersRepository.GetById(request.Id)
            ?? throw new NotFoundException(nameof(Question), request.Id.ToString());

        request.UpdateEntity(questionEntity,_dateTimeProvider);
        await _questionAndAnswersRepository.Update(questionEntity);

        return Unit.Value;
    }
}