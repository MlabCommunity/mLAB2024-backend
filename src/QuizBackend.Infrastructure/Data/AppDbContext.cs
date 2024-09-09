using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data;
public class AppDbContext : IdentityDbContext<User, Role, string>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<QuizParticipation> QuizParticipations { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}