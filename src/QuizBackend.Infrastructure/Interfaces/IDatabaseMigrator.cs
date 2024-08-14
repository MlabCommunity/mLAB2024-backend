namespace QuizBackend.Application.Interfaces
{
    public interface IDatabaseMigrator
    {
        Task EnsureMigrationAsync();
    }
}
