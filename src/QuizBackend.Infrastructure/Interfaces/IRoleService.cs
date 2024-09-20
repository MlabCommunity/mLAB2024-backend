using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Infrastructure.Interfaces;

public interface IRoleService
{
    Task AssignRole(User user, AppRole role);
    Task RemoveRole(User user, AppRole role);
}
