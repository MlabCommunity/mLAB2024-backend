using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
