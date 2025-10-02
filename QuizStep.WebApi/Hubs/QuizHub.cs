using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Infrastructure.Data;

namespace QuizStep.WebApi.Hubs
{
    public class QuizHub : Hub
    {
        private readonly ApplicationContext _context;

        public QuizHub(ApplicationContext context)
        {
            _context = context;
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