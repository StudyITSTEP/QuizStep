using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Services;

namespace QuizStep.Infrastructure.Hubs;

public class QuizMonitorHub : Hub
{
    private readonly CurrentQuizActiveUsersService _activeUsersService;
    private readonly IUser _user;
    private readonly IQuizProvider _quizProvider;

    public QuizMonitorHub(CurrentQuizActiveUsersService activeUsersService, IUser user, IQuizProvider quizProvider)
    {
        _activeUsersService = activeUsersService;
        _user = user;
        _quizProvider = quizProvider;
    }

    public override async Task OnConnectedAsync()
    {
        var context = Context.GetHttpContext();
        if (context == null) return;
        
        var quizIdString = context.GetRouteData().Values["quizId"].ToString();
        if(!int.TryParse(quizIdString, out var quizId)) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, $"quiz-{quizId}");
        var activeUsers = await _activeUsersService.GetActiveUsersByQuizIdAsync(quizId);
        await Clients.Caller.SendAsync("ActiveUsers", activeUsers);
        await base.OnConnectedAsync();
    }
}