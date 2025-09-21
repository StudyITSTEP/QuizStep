using MediatR;
using Microsoft.AspNetCore.Http;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.Quiz
{
    public class DeleteQuizCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
