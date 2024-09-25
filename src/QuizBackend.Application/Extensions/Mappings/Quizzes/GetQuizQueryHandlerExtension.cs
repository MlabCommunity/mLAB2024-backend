using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

public static class GetQuizQueryHandlerExtension
{
    public static QuizDetailsDto ToResponse(this Quiz quiz, string? shareLink, PagedDto<QuizBackend.Domain.Entities.QuizParticipation> pagedQuizParticipations)
    {
        var questionDto = quiz.Questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Answers.Select(a => new AnswerDto(
                a.Id,
                a.Content,
                a.IsCorrect)).ToList()
            )).ToList();

        var participantsDto = pagedQuizParticipations.Items.Select(participation =>
        {
            var participant = participation.Participant;
            var score = participation.QuizResult?.ScorePercentage;

            return new ParticipantDto(
                participant.DisplayName!,
                score,
                participation.Status,
                participation.ParticipationDateUtc
            );
        }).ToList();

        var pagedParticipantsDto = new PagedDto<ParticipantDto>(
            participantsDto,
            pagedQuizParticipations.TotalItemsCount,
            pagedQuizParticipations.ItemsTo - pagedQuizParticipations.ItemsFrom + 1,
            (pagedQuizParticipations.ItemsFrom - 1) / participantsDto.Count + 1 
        );

        return new QuizDetailsDto(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            shareLink,
            quiz.Availability,
            quiz.Status,
            questionDto,
            pagedParticipantsDto 
        );
    }
}