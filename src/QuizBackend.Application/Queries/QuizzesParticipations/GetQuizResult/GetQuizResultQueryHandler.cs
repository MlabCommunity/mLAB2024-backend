using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Dtos.UserAnswers;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;
using QuizBackend.Application.Extensions.Mappings.QuizParticipation;

namespace QuizBackend.Application.Queries.QuizzesParticipations.GetQuizResult;

public record QuizResultResponse(
    Guid QuizParticipationId,
    QuizDetailsDto QuizDetails,
    List<UserAnswerDto> UserAnswers,
    int TotalQuestions,
    int CorrectAnswers,
    double ScorePercentage
);
public class GetQuizResultQueryHandler : IQueryHandler<GetQuizResultQuery, QuizResultResponse>
{
    private readonly IQuizResultRepository _quizResultRepository;
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;

    public GetQuizResultQueryHandler(IQuizResultRepository quizResultRepository, IQuizParticipationRepository quizParticipationRepository, IQuestionAndAnswersRepository questionAndAnswersRepository)
    {
        _quizResultRepository = quizResultRepository;
        _quizParticipationRepository = quizParticipationRepository;
        _questionAndAnswersRepository = questionAndAnswersRepository;

    }

    public async Task<QuizResultResponse> Handle(GetQuizResultQuery request, CancellationToken cancellationToken)
    {
        var quizParticipation = await _quizParticipationRepository.GetQuizParticipation(request.QuizParticipationId);

        if (quizParticipation == null)
        {
            throw new NotFoundException(nameof(QuizParticipation), request.QuizParticipationId.ToString());
        }

        var quizResult = await _quizResultRepository.GetByQuizParticipationId(request.QuizParticipationId);

        if (quizResult == null)
        {
            throw new NotFoundException(nameof(QuizResult), request.QuizParticipationId.ToString());
        }

        var questionDtos = await GetQuestionDtos(quizParticipation);
        var userAnswersDto = GetUserAnswersDtos(quizParticipation);

        return quizResult.ToResponse(quizParticipation, questionDtos, userAnswersDto);
    }

    private async Task<List<QuestionDto>> GetQuestionDtos(QuizParticipation quizParticipation)
    {
        var questions = quizParticipation.Quiz.Questions;
        var questionDtos = new List<QuestionDto>();

        foreach (var question in questions)
        {
            var answers = await _questionAndAnswersRepository.GetAnswersByQuestionId(question.Id);
            var answerDtos = answers.Select(a => new AnswerDto(a.Id, a.Content, a.IsCorrect)).ToList();

            questionDtos.Add(new QuestionDto(question.Id, question.Title, answerDtos));
        }

        return questionDtos;
    }

    private List<UserAnswerDto> GetUserAnswersDtos(QuizParticipation quizParticipation)
    {
        return quizParticipation.UserAnswers
            .Select(ua => new UserAnswerDto(ua.QuestionId, ua.AnswerId))
            .ToList();
    }
}