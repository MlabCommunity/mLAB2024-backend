using QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;

public class CreateQuestionAndAnswersCommandHandler : ICommandHandler<CreateQuestionAndAnswersCommand, Guid>
{
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository, IDateTimeProvider dateTimeProvider)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(CreateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
    {
        var question = request.ToEntity(_dateTimeProvider);
        await _questionAndAnswersRepository.Add(question);

        return question.Id;
    }
}