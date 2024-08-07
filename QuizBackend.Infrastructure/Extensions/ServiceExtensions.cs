using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuizBackend.Domain.Entities;
using QuizBackend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddJwtExtension(configuration);

            return services;

        }

    }
}