using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Core.Interfaces
{
    public interface IAnswer
    {
        Task<Answer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Answer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Answer answer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Answer answer, CancellationToken cancellationToken = default);
        Task DeleteAsync(Answer answer, CancellationToken cancellationToken = default);
    }
}
