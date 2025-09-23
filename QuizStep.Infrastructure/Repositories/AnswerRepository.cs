using Microsoft.EntityFrameworkCore;
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
    public class AnswerRepository : IAnswer
    {
        private readonly ApplicationContext _context;

        public AnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Answer answer, CancellationToken cancellationToken = default)
        {
            await _context.Set<Answer>().AddAsync(answer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Answer answer, CancellationToken cancellationToken = default)
        {
            _context.Set<Answer>().Remove(answer);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Answer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Answer>()
                .Include(a => a.Questions)
                .ToListAsync(cancellationToken);
        }

        public async Task<Answer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Answer>()
                .Include(a => a.Questions)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Answer answer, CancellationToken cancellationToken = default)
        {
            _context.Set<Answer>().Update(answer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
