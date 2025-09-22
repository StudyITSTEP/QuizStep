using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizStep.Core.Interfaces;

namespace QuizStep.WebApi.Filters.AuthorizationFilters;

public class QuizAccessAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _policyName;

    public QuizAccessAuthorizationFilter(string policyName)
    {
        _policyName = policyName;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();
        var quizProvider = context.HttpContext.RequestServices.GetService<IQuizProvider>();
        if (quizProvider == null || authorizationService == null) return;

        int.TryParse(context.RouteData.Values["id"].ToString(), out int quizId);
        int.TryParse(context.HttpContext.Request.Query["accessCode"].ToString(), out int accessCode);
        
        if (quizId == 0) context.Result = new UnauthorizedObjectResult("Quiz id not provided");

        var quiz = await quizProvider.GetByIdAsync(quizId, new CancellationToken());
        if (quiz == null) return;
        quiz.AccessCode = accessCode;
        var authResult = await authorizationService
            .AuthorizeAsync(context.HttpContext.User, quiz, _policyName);

        if (!authResult.Succeeded)
        {
            context.Result = new UnauthorizedObjectResult("Quiz access denied");
        }
    }
}