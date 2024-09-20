using MediatR;
using Microsoft.AspNetCore.Http;
using QuizBackend.Application.Extensions;
using QuizBackend.Application.Extensions.Mappings.QuizParticipation;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Exceptions;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Commands.QuizzesParticipations.SubmitQuizAnswer;

public class SubmitQuizAnswerCommandHandler : ICommandHandler<SubmitQuizAnswerCommand, Unit>
{
    private readonly IUserAnswerRepository _userAnswerRepository;
    private readonly IQuizResultRepository _quizResultRepository;
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SubmitQuizAnswerCommandHandler(IUserAnswerRepository userAnswerRepository, IQuizResultRepository quizResultRepository, IQuestionAndAnswersRepository questionAndAnswersRepository, IQuizParticipationRepository quizParticipationRepository, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
    {
        _userAnswerRepository = userAnswerRepository;
        _quizResultRepository = quizResultRepository;
        _questionAndAnswersRepository = questionAndAnswersRepository;
        _quizParticipationRepository = quizParticipationRepository;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(SubmitQuizAnswerCommand request, CancellationToken cancellationToken)
    {
        var quizParticipation = await GetQuizParticipationById(request.QuizParticipationId);

        if (quizParticipation.Quiz.OwnerId != _httpContextAccessor.GetUserId())
            throw new BadRequestException("QuizParticipation Not found");

        if (quizParticipation.Status == QuizParticipationStatus.Stopped)
        {
            return Unit.Value;
        }

        await AddUserAnswers(request);

        var quizResultData = await CalculateQuizResultData(quizParticipation);

        await SaveOrUpdateQuizResult(quizParticipation.Id, quizResultData);

        return Unit.Value;
    }
    private async Task AddUserAnswers(SubmitQuizAnswerCommand request)
    {
        var userAnswers = request.ToEntities(_dateTimeProvider);
        await _userAnswerRepository.AddRange(userAnswers);
    }

    private async Task<QuizParticipation> GetQuizParticipationById(Guid quizParticipationId)
    {
        return await _quizParticipationRepository.GetQuizParticipation(quizParticipationId)
            ?? throw new NotFoundException(nameof(QuizParticipation), quizParticipationId.ToString());
    }

    private async Task<QuizResultData> CalculateQuizResultData(QuizParticipation quizParticipation)
    {
        var totalQuestions = quizParticipation.Quiz.Questions.Count;
        var correctAnswers = await CountCorrectAnswers(quizParticipation);

        var scorePercentage = CalculateScorePercentage(correctAnswers, totalQuestions);

        return new QuizResultData
        {
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            ScorePercentage = scorePercentage
        };
    }

    private async Task<int> CountCorrectAnswers(QuizParticipation quizParticipation)
    {
        var correctAnswersCount = 0;

        foreach (var userAnswer in quizParticipation.UserAnswers)
        {
            var correctAnswer = await _questionAndAnswersRepository.GetCorrectAnswerByQuestionId(userAnswer.QuestionId);
            if (correctAnswer != null && correctAnswer.Id == userAnswer.AnswerId)
            {
                correctAnswersCount++;
            }
        }
        return correctAnswersCount;
    }

    private double CalculateScorePercentage(int correctAnswers, int totalQuestions)
    {
        return totalQuestions > 0 ? (double)correctAnswers / totalQuestions * 100 : 0;
    }

    private async Task SaveOrUpdateQuizResult(Guid quizParticipationId, QuizResultData resultData)
    {
        var existingResult = await _quizResultRepository.GetByQuizParticipationId(quizParticipationId);

        if (existingResult != null)
        {
            UpdateQuizResult(existingResult, resultData);
            await _quizResultRepository.Update(existingResult);
        }
        else
        {
            var newQuizResult = CreateNewQuizResult(quizParticipationId, resultData);
            await _quizResultRepository.Add(newQuizResult);
        }
    }

    private void UpdateQuizResult(QuizResult existingResult, QuizResultData resultData)
    {
        existingResult.TotalQuestions = resultData.TotalQuestions;
        existingResult.CorrectAnswers = resultData.CorrectAnswers;
        existingResult.ScorePercentage = resultData.ScorePercentage;
        existingResult.CalculatedAt = _dateTimeProvider.UtcNow;
    }

    private QuizResult CreateNewQuizResult(Guid quizParticipationId, QuizResultData resultData)
    {
        return new QuizResult
        {
            QuizParticipationId = quizParticipationId,
            TotalQuestions = resultData.TotalQuestions,
            CorrectAnswers = resultData.CorrectAnswers,
            ScorePercentage = resultData.ScorePercentage,
            CalculatedAt = _dateTimeProvider.UtcNow
        };
    }

    private async Task UpdateQuizParticipationStatusToFinished(QuizParticipation quizParticipation)
    {
        quizParticipation.Status = QuizParticipationStatus.Finished;
        quizParticipation.CompletionTime = _dateTimeProvider.UtcNow;

        await _quizParticipationRepository.Update(quizParticipation);
    }

    public class QuizResultData
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
    }
}