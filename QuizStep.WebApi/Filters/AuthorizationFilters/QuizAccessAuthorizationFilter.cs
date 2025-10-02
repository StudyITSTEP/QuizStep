using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizStep.Core.Entities;
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
        if (authorizationService == null) return;

        var request = context.HttpContext.Request;
        string? code = null;
        int quizId = -1;
        try
        {
            // For JSON body
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // reset for later

            // Now you can parse JSON if needed
            var json = JsonDocument.Parse(body);

            quizId = json.RootElement.GetProperty("id").GetInt32();
            code = json.RootElement.GetProperty("accessCode").GetString();
        }
        catch (Exception)
        {
        }

        int accessCode = -1;
        if (!string.IsNullOrEmpty(code))
        {
            int.TryParse(code, out accessCode);
        }

        if (quizId == -1)
        {
            int.TryParse(context.RouteData.Values["id"].ToString(), out quizId);
        }
        
        if (quizId == -1) context.Result = new UnauthorizedObjectResult("Quiz id not provided");

        var quiz = new Quiz() { Id = quizId, AccessCode = accessCode };

        quiz.AccessCode = accessCode;
        var authResult = await authorizationService
            .AuthorizeAsync(context.HttpContext.User, quiz, _policyName);

        if (!authResult.Succeeded)
        {
            context.Result = new UnauthorizedObjectResult("Quiz access denied");
        }
    }
}