using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizStep.Application.Commands___Queries.Test;
using QuizStep.Application.DTOs.Test;
using QuizStep.Application.Interfaces;
using QuizStep.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Handlers.Test
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetByIdTestQuery, TestDto>
    {
        private readonly ITest _testRepo;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(ITest testRepo, IMapper mapper)
        {
            _testRepo = testRepo;
            _mapper = mapper;
        }

        public async Task<TestDto> Handle(GetByIdTestQuery request, CancellationToken cancellationToken)
        {
            var test = await _testRepo.GetByIdAsync(request.Id, cancellationToken);
            if (test == null)
                throw new KeyNotFoundException($"Test with Id {request.Id} not found");

            return _mapper.Map<TestDto>(test);
        }
    }

}
