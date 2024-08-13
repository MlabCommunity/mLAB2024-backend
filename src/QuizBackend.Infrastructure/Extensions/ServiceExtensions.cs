using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Domain.Entities;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+' ";
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddJwtExtension(configuration);
            services.AddAuthExtension(configuration);
            services.AddProfileExtensions();

            return services;

        }
           
    }
}
