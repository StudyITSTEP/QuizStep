using MediatR;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Quiz
{
    public class GetQuizzesByCategoryQuery : IRequest<Result<List<QuizDto>>>
    {
        public int CategoryId { get; set; }
    }
}
