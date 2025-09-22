using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuizStep.Infrastructure.Config;

namespace QuizStep.WebApi.Extentions;

public static class JwtAuthenticationService
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtConfig.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = JwtConfig.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = JwtConfig.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };

                opts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token valid for: " + context.Principal.Identity?.Name);
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }
}