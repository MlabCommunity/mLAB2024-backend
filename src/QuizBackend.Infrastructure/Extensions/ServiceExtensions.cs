using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Domain.Entities;
using QuizBackend.Infrastructure.Data;

namespace QuizBackend.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services
            .AddIdentity<User, Role>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+' ";
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddJwtExtension(configuration);
        services.AddAuthExtension(configuration);

        services.AddHttpContextAccessor();

        services.AddProfileExtensions();

        services.AddDataMigrator();

        services.AddAiClientFactoryExtension(configuration);
        services.AddKernelExtension();
        services.AddAiServiceExtension();

        services.AddRepositories();
        services.AddDateTimeProvider();
        services.AddProcessors();

        return services;
    } 
}