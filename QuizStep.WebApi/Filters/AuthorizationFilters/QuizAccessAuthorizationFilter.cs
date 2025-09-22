using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizStep.Core.Interfaces;

namespace QuizStep.WebApi.Filters.AuthorizationFilters;

public class QuizAccessAuthorizationFilter: Attribute,IAsyncAuthorizationFilter
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IQuizProvider  _quizProvider;
    public QuizAccessAuthorizationFilter(IAuthorizationService authorizationService, IQuizProvider quizProvider)
    {
        _authorizationService = authorizationService;
        _quizProvider = quizProvider;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        int.TryParse(context.RouteData.Values["id"].ToString(), out int quizId);
        if (quizId == 0) context.Result = new UnauthorizedObjectResult("Quiz id not provided");
        
        var quiz = await _quizProvider.GetByIdAsync(quizId, new CancellationToken());
        var authResult = await _authorizationService
            .AuthorizeAsync(context.HttpContext.User, quiz, "QuizAccess");

        if (!authResult.Succeeded)
        {
            context.Result = new UnauthorizedObjectResult("Quiz access denied");
        }
    }
}