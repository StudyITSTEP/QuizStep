using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Commands___Queries.Test
{
    public class DeleteTestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteTestCommand(int id) => Id = id;
    }
}
