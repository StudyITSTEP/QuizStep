using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Infrastructure.Repositories
{
    public class TestRepository : ITest
    {
        private readonly ApplicationContext _context;

        public TestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Test?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Tests.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Test test, CancellationToken cancellationToken)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Test test, CancellationToken cancellationToken)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Test test, CancellationToken cancellationToken)
        {
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
