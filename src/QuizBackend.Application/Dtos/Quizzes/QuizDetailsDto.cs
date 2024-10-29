using QuizBackend.Application.Dtos.Paged;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Dtos.Quizzes;

public record QuizDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    string? ShareLink,
    Availability Availability,
    Status Status,
    List<QuestionDto> Questions,
    PagedDto<ParticipantDto> Participants);