using Microsoft.AspNetCore.SignalR;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Services;

namespace QuizStep.Infrastructure.Hubs;

public class QuizCreatorHub: Hub
{

    private readonly CurrentQuizActiveUsersService _activeUsersService;
    private readonly IUser _user;
    private readonly IQuizProvider _quizProvider;

    public QuizCreatorHub(CurrentQuizActiveUsersService activeUsersService, IUser user,  IQuizProvider quizProvider)
    {
        _activeUsersService =  activeUsersService;
        _user = user;
        _quizProvider = quizProvider;
    }
    
    public override async Task OnConnectedAsync()
    {
        var context = Context.GetHttpContext();
        if (context == null) return;
        
        var userId = context.Request.Query["userId"].FirstOrDefault();
        
        if(userId == null) return;
        
        var user = await _user.GetUserByIdAsync(userId);
        if (user == null) return;
        
        var creatorQuizzes = await _quizProvider.GetByUserAsync(userId, new CancellationToken());
        var activeUsers = await _activeUsersService.GetActiveQuizzesByCreatorAsync(creatorQuizzes);
        await Clients.Caller.SendAsync("ActiveUsersList", activeUsers);
        await base.OnConnectedAsync();
    }
}