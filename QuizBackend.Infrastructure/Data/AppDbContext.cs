using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=QuizBackendDatabase;User Id=SA;Password=Pass@word;Encrypt=false;TrustServerCertificate=true;Integrated Security=false;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
        }
    }
}
