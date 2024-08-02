using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = 127.0.0.1; Database = QuizBackendDatabase; user id = SA; password = Pass@word; Encrypt = false; TrustServerCertificate = true; Integrated Security = false;");
        }
    }
}
