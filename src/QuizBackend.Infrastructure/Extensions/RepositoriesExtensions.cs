using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Domain.Repositories;
using QuizBackend.Infrastructure.Repositories;

namespace QuizBackend.Infrastructure.Extensions;

public static class RepositoriesExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuestionAndAnswersRepository, QuestionAndAnswersRepository>();
        services.AddScoped<IQuizParticipationRepository, QuizParticipationRepository>();
        services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
        services.AddScoped<IQuizResultRepository, QuizResultRepository>();
    }
}