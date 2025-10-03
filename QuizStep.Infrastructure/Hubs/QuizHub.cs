using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Data;

namespace QuizStep.Infrastructure.Hubs
{
    [Authorize]
    public class QuizHub : Hub
    {
        private readonly ApplicationContext _context;
        private readonly IUser _user;

        public QuizHub(ApplicationContext context, IUser user)
        {
            _context = context;
            _user = user;
        }

        public override async Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();
            if (context == null) return;
            try
            {
                var user = await _user.GetUserAsync();
                if (user == null) return;
                var routeValue = context.GetRouteData().Values["quizId"] as string;
                if (int.TryParse(routeValue, out var quizId))
                {
                    await Groups.AddToGroupAsync(user.Id, $"quiz-{quizId}");
                }

                Console.WriteLine("Connected: " + user.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await base.OnConnectedAsync();
        }

        public async Task SendCurrentQuestion(int currentQuestion)
        {
        }

        public async Task JoinAsAdmin(int quizId, string adminId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null) return;

            if (quiz.CreatorId != adminId)
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, $"quiz-{quizId}-admin");
        }

        public async Task JoinAsParticipant(int quizId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"quiz-{quizId}-participants");
        }

        public async Task SendProgress(int quizId, QuizParticipantProgressDto dto)
        {
            await Clients.Group($"quiz-{quizId}-admin")
                .SendAsync("ReceiveParticipantProgress", dto);
        }
    }
}