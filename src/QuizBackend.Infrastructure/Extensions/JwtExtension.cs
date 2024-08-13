using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QuizBackend.Infrastructure.Extensions
{
    public static class JwtExtension
    {
        public static void AddJwtExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];

            var key = secretKey != null
                ? Encoding.UTF8.GetBytes(secretKey)
                : throw new InvalidConfigurationException("JWT secret key is not configured or is empty.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["AccessToken"];
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
