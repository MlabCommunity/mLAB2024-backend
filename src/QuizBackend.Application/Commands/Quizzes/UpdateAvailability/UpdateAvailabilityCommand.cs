using QuizBackend.Application.Interfaces.Messaging;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Application.Commands.Quizzes.UpdateAvailability
{
    public record UpdateAvailabilityCommand(Guid Id, Availability Availability) : ICommand<UpdateAvailabilityResponse>;
}