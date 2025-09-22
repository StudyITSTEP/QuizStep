using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizStep.Core.Primitives;

namespace QuizStep.Core.Interfaces
{
    public interface IQuizProvider
    {
        Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Quiz>> GetQuizzesAsync(CancellationToken cancellationToken);
        Task AddAsync(Quiz quiz, CancellationToken cancellationToken);
        Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken);
        Task DeleteAsync(Quiz quiz, CancellationToken cancellationToken);
        Task<int?> GetAccessCodeAsync(string userId, CancellationToken cancellationToken);
        Task<Question?> CreateQuestionAsync(Question question, CancellationToken cancellationToken);
        Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId, CancellationToken cancellationToken);
        Task<Question?> GetQuestionByIdAsync(int questionId, CancellationToken cancellationToken);
        Task<Result> RemoveQuestionAsync(int questionId, CancellationToken cancellationToken);
        Task<Question?> UpdateQuestionAsync(Question question, CancellationToken cancellationToken);
    }
}
