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
    public class CreateTestCommand : IRequest<int>
    {
        public TestDto Test { get; set; }
        public CreateTestCommand(TestDto test) { Test = test; }
    }
}
