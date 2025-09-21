using MediatR;
using QuizStep.Application.DTOs.Test;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Test
{
    public class GetByIdTestQuery : IRequest<Result<TestDto>>
    {
        public int Id { get; set; }
    }
}