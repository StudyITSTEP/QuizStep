using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;
using QuizStep.Infrastructure.Hubs;

namespace QuizStep.Infrastructure.Services;

public class ActiveUser
{
    public string UserId { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public int CurrentQuestion { get; set; }
    public bool OnPage { get; set; }
    public int TotalQuestions { get; set; }
}

public class CurrentQuizActiveUsersService
{
    private readonly ConcurrentDictionary<int, List<ActiveUser>> _activeUsers = new();
    private readonly IHubContext<ActiveUsersHub> _activeUsersHub;
    private readonly IHubContext<QuizMonitorHub> _monitorHub;

    public CurrentQuizActiveUsersService(IHubContext<ActiveUsersHub> activeUsersHub,
        IHubContext<QuizMonitorHub> monitorHub)
    {
        _activeUsersHub = activeUsersHub;
        _monitorHub = monitorHub;
    }
    
    public Task SetActiveUserAsync(int quizId, ActiveUser user)
    {
        _activeUsers[quizId] = new List<ActiveUser> { user };
        return Task.CompletedTask;
    }

    public async Task<List<ActiveUser>?> GetActiveUsersByQuizIdAsync(int quizId)
    {
        if (_activeUsers.ContainsKey(quizId))
            return _activeUsers[quizId];
        return null;
    }

    public async Task<List<ActiveQuizDto>> GetActiveQuizzesByCreatorAsync(IEnumerable<Quiz> quizzes)
    {
        var activeQuizzes = new List<ActiveQuizDto>();
        foreach (var quiz in quizzes)
        {
            if (_activeUsers.ContainsKey(quiz.Id))
            {
                activeQuizzes.Add(new ActiveQuizDto(quiz.Id, quiz.Name!));
            }
        }

        return activeQuizzes;
    }

    public Task ChangeQuestionOrOnPageOfActiveUserAsync(int quizId, string userId,
        int question = -1, bool? onPage = null)
    {
        var activeUser = _activeUsers[quizId].FirstOrDefault(x => x.UserId == userId);
        if (activeUser == null) return Task.CompletedTask;
        if (question != -1)
            activeUser.CurrentQuestion = question;
        if (onPage != null)
        {
            activeUser.OnPage = onPage.Value;
        }

        return Task.CompletedTask;
    }

    public Task RemoveActiveUserAsync(int quizId, string userId)
    {
        var activeUser = _activeUsers[quizId].FirstOrDefault(x => x.UserId == userId);
        if (activeUser != null)
        {
            _activeUsers[quizId].Remove(activeUser);
        }

        return Task.CompletedTask;
    }
    
    public async Task NotifyQuestionChanged(int quizId, string userId, int question)
    {
        await _monitorHub.Clients.Group($"quiz-{quizId}")
            .SendAsync("ActiveUserCurrentQuestion", userId, question);
    }
}