using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
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
    [NotMapped] public string? ConnectionId { get; set; }
    [NotMapped] public string? CreatorId { get; set; }
    [NotMapped] public string? QuizName { get; set; }
}

/*
 *  Channels map:
 *      ActiveUserFinished          - user disconnected from signalR
 *      ActiveUser                  - sending signal user
 *      ActiveUserCurrentQuestion   - sending current question for user
 *      ActiveUsers                 - sending all active users
 */

public class CurrentQuizActiveUsersService
{
    private readonly ConcurrentDictionary<int, List<ActiveUser>> _activeUsers = new();
    private readonly IHubContext<ActiveUsersHub> _activeUsersHub;
    private readonly IHubContext<QuizMonitorHub> _monitorHub;
    private readonly IHubContext<QuizCreatorHub> _creatorHub;

    public CurrentQuizActiveUsersService(IHubContext<ActiveUsersHub> activeUsersHub,
        IHubContext<QuizMonitorHub> monitorHub, IHubContext<QuizCreatorHub> creatorHub)
    {
        _activeUsersHub = activeUsersHub;
        _monitorHub = monitorHub;
        _creatorHub = creatorHub;
    }

    public Task SetActiveUserAsync(int quizId, ActiveUser user)
    {
        if (_activeUsers.ContainsKey(quizId))
        {
           _activeUsers[quizId].Add(user);
        }
        else
        {
            _activeUsers.TryAdd(quizId, new List<ActiveUser> { user });
        }
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

    public async Task RemoveActiveUserAsync(string connectionId)
    {
        foreach (KeyValuePair<int, List<ActiveUser>> entry in _activeUsers)
        {
            var activeUser = entry.Value.Find(w => w.ConnectionId == connectionId);
            if (activeUser != null)
            {
                _activeUsers[entry.Key].Remove(activeUser);
                
                await _activeUsersHub.Groups.RemoveFromGroupAsync(connectionId, $"quiz-{entry.Key}");
                var activeUsers = await GetActiveUsersByQuizIdAsync(entry.Key);
                
                await _monitorHub.Clients.Group($"quiz-{entry.Key}")
                    .SendAsync("ActiveUserFinished", activeUser.UserId);

                if (!entry.Value.Any())
                {
                    _activeUsers.TryRemove(entry.Key, out _);
                    await _creatorHub.Clients.Group($"{activeUser.CreatorId}").SendAsync("QuizEmpty", entry.Key);
                }
            }
            
        }
    }

    public async Task NotifyActiveUserJoinedAsync(int quizId, ActiveUser user)
    {
        await _creatorHub.Clients.Group(user.CreatorId!).SendAsync("ActiveQuiz",
            new ActiveQuizDto(quizId, user.QuizName!));

        await _monitorHub.Clients.Group($"quiz-{quizId}")
            .SendAsync("ActiveUser", user);
    }

    public async Task NotifyQuestionChanged(int quizId, string userId, int question)
    {
        await _monitorHub.Clients.Group($"quiz-{quizId}")
            .SendAsync("ActiveUserCurrentQuestion", userId, question);
    }
}