using Microsoft.AspNetCore.Authorization;
using QuizStep.Application.AccessHandlers.Handlers;

namespace QuizStep.WebApi.Extentions;

public static class AuthorizationHandlerServices
{
    public static IServiceCollection  AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, IsQuizOwnerHandler>();
        services.AddScoped<IAuthorizationHandler, QuizAccessHandler>();
        return services;
    }
}