using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Infrastructure.Data;
using QuizBackend.Infrastructure.Interfaces;

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
