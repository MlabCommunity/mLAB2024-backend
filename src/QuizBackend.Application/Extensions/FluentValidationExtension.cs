using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QuizBackend.Application.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}