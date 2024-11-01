﻿using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Application.Dtos.Quizzes;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Application.Extensions.Mappings.Quizzes;

public static class GetQuizQueryHandlerExtension
{
    public static QuizDetailsDto ToResponse(this Quiz quiz, string? shareLink, (List<QuizBackend.Domain.Entities.QuizParticipation> quizParticipations, int totalCount, int pageSize, int pageNumber) data)
    {
        var (quizParticipations, totalCount, pageSize, pageNumber) = data;

        var questionDto = quiz.Questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Answers.Select(a => new AnswerDto(
                a.Id,
                a.Content,
                a.IsCorrect)).ToList()
        )).ToList();

        var participantsDto = quizParticipations.Select(participation =>
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
            totalCount,
            pageSize,
            pageNumber
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