using MediatR;
using Microsoft.AspNetCore.Http;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Test
{
    public class DeleteTestCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }
}
