using QuizStep.Core.Entities;
using QuizStep.Core.Primitives;

namespace QuizStep.Core.Interfaces;

public interface IQuizResultProvider
{
    public Task<Result<int>> GetTotalParticipantsAsync(int quizId);
    public Task<Result<decimal>> GetAverageScoreByQuizAsync(int quizId);
    public Task<Result<IEnumerable<QuizResult>>>  GetQuizResultsByUserIdAsync(string userId);
}