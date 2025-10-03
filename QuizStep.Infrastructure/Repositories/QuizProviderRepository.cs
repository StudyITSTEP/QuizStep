using QuizStep.Core.Entities;
using QuizStep.Core.Interfaces;
using QuizStep.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizStep.Core.Enums;
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
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }


        public async Task<IEnumerable<Quiz>> GetByUserAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Quizzes.Where(q => q.CreatorId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync(CancellationToken cancellationToken)
        {
            return await _context.Quizzes.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            if (quiz.Access == QuizAccess.WithCodeIOnly)
            {
                quiz.AccessCode = RandomNumberGenerator.GetInt32(10000, 99999);
            }

            _context.Quizzes.Add(quiz); // ✅ Only this
            await _context.SaveChangesAsync(cancellationToken);
            foreach (var q in quiz.Questions)
            {
                var index = 0;
                foreach (var answer in q.Answers) // or Id if set
                {
                    if (index == q.CorrectAnswerIndex)
                    {
                        var res = _context.QuestionAnswers
                            .FirstOrDefault(qa => qa.QuestionId == q.Id && qa.AnswerId == answer.Id);
                        res.IsCorrect = true;
                        _context.QuestionAnswers.Update(res);
                    }

                    index++;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task UpdateAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            if (quiz.Access == QuizAccess.WithCodeIOnly)
            {
                quiz.AccessCode = new Random().Next(10000, 99999);
            }

            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Quiz quiz, CancellationToken cancellationToken)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int?> GetAccessCodeAsync(string userId, CancellationToken cancellationToken)
        {
            var quiz = await _context.Quizzes.FindAsync(new object[] { userId }, cancellationToken);
            return quiz?.AccessCode;
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

        public async Task<QuizResult?> GetQuizResultAsync(string userId, int quizId,
            CancellationToken cancellationToken)
        {
            return await _context.QuizResults
                .FirstOrDefaultAsync(q => q.UserId == userId && q.QuizId == quizId,
                    cancellationToken);
        }

        public async Task<Result> SetQuizResultAsync(QuizResult result, CancellationToken cancellationToken)
        {
            var qr = await _context.QuizResults
                .FirstOrDefaultAsync(
                    r => r.QuizId == result.QuizId && r.UserId == result.UserId, cancellationToken);
            if (qr != null)
            {
                qr.Score = result.Score;
            }
            else
            {
                await _context.QuizResults.AddAsync(result, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<IEnumerable<QuizResult>> GetQuizResultsByUserIdAsync(string userId,
            CancellationToken cancellationToken)
        {
            return await _context.QuizResults.Where(q => q.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<decimal> ResolveScoreAsync(int quizId, Dictionary<int, int> answers,
            CancellationToken cancellationToken)
        {
            var correctCount = 0;
            var totalQuestions = _context.Questions.Count(q => q.QuizId == quizId);
            foreach (var answer in answers)
            {
                var correct = await _context.QuestionAnswers
                    .FirstOrDefaultAsync(q => q.Question.Id == answer.Key && q.IsCorrect, cancellationToken);
                if (correct != null && correct.AnswerId == answer.Value)
                {
                    correctCount++;
                }
            }

            var result = (correctCount * 100) / (decimal)totalQuestions;
            result = Decimal.Round(result, 2);
            return result;
        }
    }
}