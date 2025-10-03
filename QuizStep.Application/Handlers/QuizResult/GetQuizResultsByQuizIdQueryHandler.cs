using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.QuizResult;

public class GetQuizResultsByQuizIdQueryHandler: IRequestHandler<GetQuizResultsByQuizIdQuery, IEnumerable<QuizResultDto>>
{
    private readonly IQuizResultProvider _quizResultProvider;
    private readonly IMapper _mapper;

    public GetQuizResultsByQuizIdQueryHandler(IMapper mapper,  IQuizResultProvider quizResultProvider)
    {
        _quizResultProvider = quizResultProvider;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<QuizResultDto>> Handle(GetQuizResultsByQuizIdQuery request, CancellationToken cancellationToken)
    {
       var quizzes =await  _quizResultProvider.GetQuizResultsByQuizIdAsync(request.QuizId);
       return _mapper.Map<IEnumerable<QuizResultDto>>(quizzes.Value);
    }
}