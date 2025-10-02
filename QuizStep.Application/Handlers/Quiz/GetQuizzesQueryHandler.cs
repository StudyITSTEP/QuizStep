using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Enums;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Quiz;

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, IEnumerable<QuizDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IQuizProvider _quizProvider;

    public GetQuizzesQueryHandler(IUser user, IQuizProvider quizProvider, IMapper mapper,  IMediator mediator)
    {
        _quizProvider = quizProvider;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<QuizDetailsDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        var result = await _quizProvider.GetQuizzesAsync( cancellationToken);
        var dto = new List<QuizDetailsDto>();
        foreach (var r in result)
        {
            var details = await _mediator.Send(new GetQuizDetailsQuery(r.Id), cancellationToken);
            if (details && details.Value != null)
            {
                var q = details.Value;
                dto.Add(details.Value);
            }
        }

        return dto;
    }
}