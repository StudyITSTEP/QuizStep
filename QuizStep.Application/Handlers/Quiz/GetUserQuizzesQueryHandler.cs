using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz;

public class GetUserQuizzesQueryHandler : IRequestHandler<GetUserQuizzesQuery, IEnumerable<QuizDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;
    private readonly IMediator _mediator;

    public GetUserQuizzesQueryHandler(IQuizProvider quizProvider, IMapper mapper, IMediator mediator)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<QuizDetailsDto>> Handle(GetUserQuizzesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _quizProvider.GetByUserAsync(request.userId, cancellationToken);
        var dto = new List<QuizDetailsDto>();
        foreach (var r in result)
        {
            var details = await _mediator.Send(new GetQuizDetailsQuery(r.Id), cancellationToken);
            if (details && details.Value != null)
            {
                var q = details.Value;
                q.AccessCode = r.AccessCode ?? 0;
                dto.Add(details.Value);
            }
        }

        return dto;
    }
}