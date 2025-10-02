using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Core.Interfaces
{
    public interface IQuestionAnswer
    {
        Task<IEnumerable<QuestionAnswer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<QuestionAnswer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(QuestionAnswer questionAnswer, CancellationToken cancellationToken = default);
        Task UpdateAsync(QuestionAnswer questionAnswer, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionAnswer>> GetByQuestionIdAsync(int questionId, CancellationToken cancellationToken = default);

        Task<IEnumerable<QuestionAnswer>> GetByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default);
    }
}
