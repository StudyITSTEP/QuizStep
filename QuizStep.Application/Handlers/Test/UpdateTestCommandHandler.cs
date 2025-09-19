using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Id} not found.");

            if (!string.IsNullOrEmpty(request.Name))
                test.Name = request.Name;

            if (request.Description != null)
                test.Description = request.Description;

            if (request.CategoryId.HasValue)
                test.CategoryId = request.CategoryId.Value;

            if (request.Access.HasValue)
                test.Access = request.Access.Value;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
