using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;
using QuizBackend.Domain.Repositories;

namespace QuizBackend.Application.Queries.QuizzesParticipations.GetUserAnswer;

public record UserAnswerHistory(Guid QuestionId, Guid AnswerId);
public record QuizResultHistory(int TotalQuestions, int CorrectAnswers, double ScorePercentage);
public record Answer(Guid Id, string Content, bool IsCorrect);
public record Question(Guid Id, string Title, List<Answer> Answers);
public record QuizParticipationHistoryResponse(
    Guid QuizId,
    string QuizTitle,
    string? QuizDescription,
    DateTime ParticiptionDateUtc,
    QuizParticipationStatus Status,
    List<QuestionDto> Questions, 
    List<UserAnswerHistory> UserAnswers,
    QuizResultHistory? QuizResult);
public class GetUserQuizHistoryQueryHandler : IQueryHandler<GetUserQuizHistoryQuery, List<QuizParticipationHistoryResponse>>
{
    private readonly IQuizParticipationRepository _quizParticipationRepository;
    private readonly IQuestionAndAnswersRepository _questionAndAnswersRepository;

    public GetUserQuizHistoryQueryHandler(IQuizParticipationRepository quizParticipationRepository, IQuestionAndAnswersRepository questionAndAnswersRepository)
    {
        _quizParticipationRepository = quizParticipationRepository;
        _questionAndAnswersRepository = questionAndAnswersRepository;
    }

    public async Task<List<QuizParticipationHistoryResponse>> Handle(GetUserQuizHistoryQuery request, CancellationToken cancellationToken)
    {
        var quizParticipations = await _quizParticipationRepository.GetByParticipantId(request.ParticipantId);

        var result = new List<QuizParticipationHistoryResponse>();

        foreach (var participation in quizParticipations)
        {
            var questions = await _questionAndAnswersRepository.GetQuestionsByQuizId(participation.QuizId);

            var questionDtos = questions.Select(q => new QuestionDto(
                q.Id,
                q.Title,
                q.Answers.Select(a => new AnswerDto(a.Id, a.Content, a.IsCorrect)).ToList()
            )).ToList();

            var userAnswersDtos = participation.UserAnswers.Select(answer => new UserAnswerHistory(
                answer.QuestionId,
                answer.AnswerId
            )).ToList();

            var quizResultDto = participation.QuizResult != null
                ? new QuizResultHistory(
                    participation.QuizResult.TotalQuestions,
                    participation.QuizResult.CorrectAnswers,
                    participation.QuizResult.ScorePercentage
                )
                : null;

            result.Add(new QuizParticipationHistoryResponse(
                participation.QuizId,
                participation.Quiz.Title,
                participation.Quiz.Description,
                participation.ParticipationDateUtc,
                participation.Status,
                questionDtos,
                userAnswersDtos,
                quizResultDto
            ));
        }
        return result;
    }
}