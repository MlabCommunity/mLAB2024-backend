using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
