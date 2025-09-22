using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;

namespace QuizStep.Application.Handlers.Quiz;

public class GetQuizzesQueryHandler: IRequestHandler<GetQuizzesQuery, IEnumerable<QuestionDto>>
{
    public Task<IEnumerable<QuestionDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}