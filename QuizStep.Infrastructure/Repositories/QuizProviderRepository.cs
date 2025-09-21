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
    public class QuizProviderRepository : IQuizProvider
    {
        private readonly ApplicationContext _context;

        public QuizProviderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Quiz?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Tests.FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Tests.Add(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Tests.Update(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Tests.Remove(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
