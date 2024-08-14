using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class DataMigratorExtension
    {
        public static void AddDataMigrator(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}
