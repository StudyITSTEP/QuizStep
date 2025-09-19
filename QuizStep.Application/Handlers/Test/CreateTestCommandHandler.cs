using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<QuizStep.Core.Entities.Test>(request.Test);

            _context.Tests.Add(test);
            await _context.SaveChangesAsync(cancellationToken);

            return test.Id;
        }
    }
}
