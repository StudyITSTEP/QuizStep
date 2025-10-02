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

        int quizId = 0;
        int accessCode = 0;

        // 1. Try getting id from route data
        if (context.RouteData.Values.TryGetValue("id", out var idValue) &&
            int.TryParse(idValue?.ToString(), out var routeId))
        {
            quizId = routeId;
        }

        // 2. Enable buffering and read request body as JSON
        context.HttpContext.Request.EnableBuffering();
        using (var reader = new StreamReader(context.HttpContext.Request.Body, leaveOpen: true))
        {
            var bodyString = await reader.ReadToEndAsync();
            context.HttpContext.Request.Body.Position = 0; // reset stream

            if (!string.IsNullOrWhiteSpace(bodyString))
            {
                try
                {
                    var bodyJson = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(bodyString);

                    if (quizId == 0 && bodyJson != null && bodyJson.TryGetValue("id", out var bodyId))
                    {
                        if (bodyId.TryGetInt32(out var parsedId))
                            quizId = parsedId;
                    }

                    if (bodyJson != null && bodyJson.TryGetValue("accessCode", out var bodyAccess))
                    {
                        if (bodyAccess.TryGetInt32(out var parsedAccess))
                            accessCode = parsedAccess;
                    }
                }
                catch (JsonException)
                {
                    context.Result = new BadRequestObjectResult("Invalid request body");
                    return;
                }
            }
        }

        // 3. Validate quizId
        if (quizId == 0)
        {
            context.Result = new UnauthorizedObjectResult("Quiz id not provided");
            return;
        }

        var quiz = new Quiz() {Id = quizId, AccessCode = accessCode};

        var authResult = await authorizationService
            .AuthorizeAsync(context.HttpContext.User, quiz, _policyName);

        if (!authResult.Succeeded)
        {
            context.Result = new UnauthorizedObjectResult("Quiz access denied");
        }
    }
}