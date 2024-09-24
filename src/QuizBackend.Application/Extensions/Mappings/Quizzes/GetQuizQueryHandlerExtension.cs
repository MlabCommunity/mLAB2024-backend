using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

public static class GetQuizQueryHandlerExtension
{
    public static QuizDetailsDto ToResponse(this Quiz quiz, string? shareLink, ICollection<QuizBackend.Domain.Entities.QuizParticipation> quizParticipations)
    {
        var questionDto = quiz.Questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Answers.Select(a => new AnswerDto(
                a.Id,
                a.Content,
                a.IsCorrect)).ToList()
            )).ToList();

        var participantsDto = quizParticipations.Where(participation => participation.QuizId == quiz.Id).Select(participation =>
        {
            var participant = participation.Participant;
            var score = participation.QuizResult?.ScorePercentage; 

            return new ParticipantDto(
                participant.DisplayName ?? "Anonymous",
                score,
                participation.Status,
                participation.ParticipationDateUtc
            );
        }).ToList();

        return new QuizDetailsDto(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            shareLink,
            quiz.Availability,
            quiz.Status,
            questionDto,
            participantsDto
        );
    }
}