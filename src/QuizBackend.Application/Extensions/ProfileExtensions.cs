using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizBackend.Application.Extensions
{
    public static class ProfileExtensions
    {
        public static void AddProfileExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IProfileService,  ProfileService>();
            services.AddHttpContextAccessor();
        }
    }
}
