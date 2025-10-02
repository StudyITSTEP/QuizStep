using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Interfaces;
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
    public class QuestionAnswerRepository : IQuestionAnswer
    {
        private readonly ApplicationContext _context;

        public QuestionAnswerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.QuestionAnswers
                .Include(qa => qa.Question)
                .Include(qa => qa.Answer)
                .ToListAsync(cancellationToken);
        }

        public async Task<QuestionAnswer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.QuestionAnswers
                .Include(qa => qa.Question)
                .Include(qa => qa.Answer)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(QuestionAnswer questionAnswer, CancellationToken cancellationToken = default)
        {
            await _context.QuestionAnswers.AddAsync(questionAnswer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(QuestionAnswer questionAnswer, CancellationToken cancellationToken = default)
        {
            _context.QuestionAnswers.Update(questionAnswer);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.QuestionAnswers.FindAsync(new object[] { id }, cancellationToken);
            if (entity is not null)
            {
                _context.QuestionAnswers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetByQuestionIdAsync(int questionId, CancellationToken cancellationToken = default)
        {
            return await _context.QuestionAnswers
                .Include(qa => qa.Answer)
                .Where(qa => qa.QuestionId == questionId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<QuestionAnswer>> GetByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default)
        {
            return await _context.QuestionAnswers
                .Include(qa => qa.Question)
                .Where(qa => qa.AnswerId == answerId)
                .ToListAsync(cancellationToken);
        }
    }
}
