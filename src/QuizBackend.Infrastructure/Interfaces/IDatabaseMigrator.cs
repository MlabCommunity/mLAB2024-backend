namespace QuizBackend.Infrastructure.Interfaces
{
    public interface IDatabaseMigrator
    {
        Task EnsureMigrationAsync();
    }
}