using MediatR;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    internal class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = new QuizStep.Core.Entities.Test
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                CreatorId = request.CreatorId,
                Access = request.Access
            };

            _context.Tests.Add(test);

            await _context.SaveChangesAsync(cancellationToken);

            return test.Id;
        }
    }
}
