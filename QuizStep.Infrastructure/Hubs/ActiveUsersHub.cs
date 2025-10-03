using Microsoft.AspNetCore.SignalR;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Services;

namespace QuizStep.Infrastructure.Hubs;

public class ActiveUsersHub: Hub
{
    private readonly CurrentQuizActiveUsersService _activeUsersService;
    private readonly IUser _user;
    private readonly IQuizProvider _quizProvider;

    public ActiveUsersHub(CurrentQuizActiveUsersService activeUsersService, IUser user,  IQuizProvider quizProvider)
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
        var quizIdString = context.Request.Query["quizId"].FirstOrDefault();
        if(!int.TryParse(quizIdString, out var quizId)) return;
        
        if(userId == null) return;
        
        var user = await _user.GetUserByIdAsync(userId);
        if (user == null) return;
        var quiz = await _quizProvider.GetByIdAsync(quizId, CancellationToken.None);
        var activeUser = new ActiveUser
        {
            UserId = userId,
            Email = user.Email,
            CurrentQuestion = 1,
            FullName = user.FirstName + " " + user.LastName,
            OnPage = true,
            TotalQuestions = quiz.Questions.Count,
            ConnectionId = Context.ConnectionId,
            CreatorId = quiz.CreatorId,
            QuizName = quiz.Name
        };
        
        await _activeUsersService.SetActiveUserAsync(quizId, activeUser);
        await Groups.AddToGroupAsync(Context.ConnectionId, $"quiz-{quizId}");
        await _activeUsersService.NotifyActiveUserJoinedAsync(quizId, activeUser);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await _activeUsersService.RemoveActiveUserAsync(Context.ConnectionId);
    }
    
    public async Task SetCurrentQuestion(string userId, int quizId, int currentQuestion)
    {
        await _activeUsersService.ChangeQuestionOrOnPageOfActiveUserAsync(quizId, userId, currentQuestion);
        await _activeUsersService.NotifyQuestionChanged(quizId, userId, currentQuestion);
    }
    public async Task SetOnPage(string userId, int quizId, bool onPage)
    {
        await _activeUsersService.ChangeQuestionOrOnPageOfActiveUserAsync(quizId, userId, -1, onPage);
        await Clients.Group($"quiz-{quizId}").SendAsync("ActiveUserOnPage",userId, onPage);
    }
}