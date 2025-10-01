using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Enums;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.Quiz;

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, IEnumerable<QuizDto>>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public GetQuizzesQueryHandler(IUser user, IQuizProvider quizProvider, IMapper mapper)
    {
        _user = user;
        _quizProvider = quizProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        var user = await _user.GetUserAsync();
        var quizzes = await _quizProvider.GetQuizzesAsync(cancellationToken);
        var result = quizzes
            .Where(q => q.CreatorId == user.Id
                             || q.Access == QuizAccess.Public);
        
        return _mapper.Map<IEnumerable<QuizDto>>(result);
    }
}