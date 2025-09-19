using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.DTOs.Test;
using QuizStep.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetByIdTestQuery, TestDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestDto> Handle(GetByIdTestQuery request, CancellationToken cancellationToken)
        {
            var test = await _context.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Id} not found");

            return _mapper.Map<TestDto>(test);
        }
    }

}
