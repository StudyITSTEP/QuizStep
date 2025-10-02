using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;
using QuizStep.Infrastructure.Data;

namespace QuizStep.Infrastructure.Repositories;

public class QuizResultProvider : IQuizResultProvider
{
    private readonly ApplicationContext _context;

    public QuizResultProvider(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> GetTotalParticipantsAsync(int quizId)
    {
        return _context.QuizResults.Where(q => q.QuizId == quizId).Count();
    }

    public async Task<Result<decimal>> GetAverageScoreByQuizAsync(int quizId)
    {
        var all = await _context.QuizResults.Where(q => q.QuizId == quizId).Select(q => new
        {
            score = q.Score
        }).ToListAsync();
        return all.Any() ? all.Average(s => s.score) : 0;
    }

    public async Task<Result<IEnumerable<QuizResult>>> GetQuizResultsByUserIdAsync(string userId)
    {
        return await _context.QuizResults.Where(qr => qr.UserId == userId).ToListAsync();
    }
}