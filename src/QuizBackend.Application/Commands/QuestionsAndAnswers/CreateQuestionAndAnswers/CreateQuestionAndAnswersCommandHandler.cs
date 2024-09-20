using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.QuestionAndAnswers;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuestionsAndAnswers.CreateQuestionAndAnswers;

public class CreateQuestionAndAnswersCommandHandler : ICommandHandler<CreateQuestionAndAnswersCommand, Guid>
{
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateQuestionAndAnswersCommandHandler(IQuestionAndAnswersRepository questionAndAnswersRepository, IQuizRepository quizRepository, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
    {
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _quizRepository = quizRepository;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> Handle(CreateQuestionAndAnswersCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.Get(request.QuizId, cancellationToken)
            ?? throw new NotFoundException(nameof(Quiz), request.QuizId.ToString());

        if (quiz.OwnerId != _httpContextAccessor.GetUserId())
            throw new ForbidException(
                "You do not have permission to create this resource",
                resourceName: nameof(Question),
                actionAttempted: "Create");

        var question = request.ToEntity(_dateTimeProvider);
        await _questionAndAnswersRepository.Add(question);

        return question.Id;
    }
}