using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Core.Interfaces
{
    public interface IQuizProvider
    {
        Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Quiz quiz, CancellationToken cancellationToken);
        Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken);
        Task DeleteAsync(Quiz quiz, CancellationToken cancellationToken);
    }
}
