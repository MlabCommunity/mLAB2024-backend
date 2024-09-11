using MediatR;
using QuizBackend.Application.Extensions.Mappings.UserAnswers;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;
public class SubmitQuizAnswerCommandHandler : ICommandHandler<SubmitQuizAnswerCommand, Unit>
{
    private readonly IUserAnswerRepository _userAnswerRepository;
    private readonly IQuizResultRepository _quizResultRepository;

    public SubmitQuizAnswerCommandHandler(IUserAnswerRepository userAnswerRepository, IQuizResultRepository quizResultRepository)
    {
        _userAnswerRepository = userAnswerRepository;
        _quizResultRepository = quizResultRepository;
    }

    public async Task<Unit> Handle(SubmitQuizAnswerCommand request, CancellationToken cancellationToken)
    {
       // TODO add to extension created at and updated at 
    }
}