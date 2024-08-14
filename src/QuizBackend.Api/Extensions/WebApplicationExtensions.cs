using QuizBackend.Infrastructure.Interfaces;

namespace QuizBackend.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task EnsureDatabaseMigratedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
            await migrator.EnsureMigrationAsync();
        }
    }
}
