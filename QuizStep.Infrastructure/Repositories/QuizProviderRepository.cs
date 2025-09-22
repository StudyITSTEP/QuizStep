using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Primitives;

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
            => await _context.Quizzes.FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync(CancellationToken cancellationToken)
        {
            return await _context.Quizzes.ToListAsync(cancellationToken);
                
        }

        public async Task AddAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Question?> CreateQuestionAsync(Question question, CancellationToken cancellationToken)
        {
            var quiz = await GetByIdAsync(question.QuizId, cancellationToken);
            if (quiz == null) return null;

            var q = _context.Questions.Add(question);
            await _context.SaveChangesAsync(cancellationToken);
            return q.Entity;
        }

        public async Task<IEnumerable<Question>> GetQuestionsByQuizAsync(int quizId,
            CancellationToken cancellationToken)
        {
            return await _context.Questions.Where(q => q.QuizId == quizId).ToListAsync(cancellationToken);
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId, CancellationToken cancellationToken)
        {
            return await _context.Questions.FindAsync(new object[] { questionId }, cancellationToken);
        }

        public async Task<Result> RemoveQuestionAsync(int questionId, CancellationToken cancellationToken)
        {
            var q = await _context.Questions.FindAsync(new object[] { questionId }, cancellationToken);
            if (q == null) return QueryError.EntityNotExist;
            _context.Questions.Remove(q);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Question?> UpdateQuestionAsync(Question question, CancellationToken cancellationToken)
        {
            var quiz = await GetByIdAsync(question.QuizId, cancellationToken);
            if (quiz == null) return null;

            var q = _context.Questions.Update(question);
            await _context.SaveChangesAsync(cancellationToken);
            return q.Entity;
        }
    }
}