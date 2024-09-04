using MediatR;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.DeleteQuestionAndAnswers;

public class DeleteQuestionAndAnswersCommandHandler : ICommandHandler<DeleteQuestionAndAnswersCommand, Unit>
{
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;

    public DeleteQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
    }

    public async Task<Unit> Handle(DeleteQuestionAndAnswersCommand request, CancellationToken cancellation)
    {
        await _questionAndAnswersRepository.Delete(request.Id);

        return Unit.Value;
    }
}