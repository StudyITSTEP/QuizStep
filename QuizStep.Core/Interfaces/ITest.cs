using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Core.Interfaces
{
    public interface ITest
    {
        Task<Test?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Test test, CancellationToken cancellationToken);
        Task UpdateAsync(Test test, CancellationToken cancellationToken);
        Task DeleteAsync(Test test, CancellationToken cancellationToken);
    }
}
