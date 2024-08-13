﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizBackend.Application.Interfaces;
using QuizBackend.Infrastructure.Services;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class AuthExtension
    {
        public static void AddAuthExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}