using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _context.Tests.FindAsync(new object[] { request.Id }, cancellationToken);

            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Id} not found");

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
