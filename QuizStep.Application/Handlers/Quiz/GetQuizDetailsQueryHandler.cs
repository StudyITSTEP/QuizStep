using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz;

public class GetQuizDetailsQueryHandler: IRequestHandler<GetQuizDetailsQuery, Result<QuizDetailsDto>>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;

    public GetQuizDetailsQueryHandler(IUser user, IQuizProvider quizProvider, IMapper mapper)
    {
        _user = user;
        _quizProvider = quizProvider;
        _mapper = mapper;
    }

    public async Task<Result<QuizDetailsDto>> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizProvider.GetByIdAsync(request.QuizId, cancellationToken);
        if (quiz == null) return QueryError.EntityNotExist;

        var dto = new QuizDetailsDto();
        
        var creator = await _user.GetUserByIdAsync(quiz.CreatorId);
        var fullName = creator.FirstName + " " + creator.LastName;
        dto.CreatorName = fullName;
        dto.CreatorEmail = creator.Email;
        dto.TotalQuestions = quiz.Questions.Count;
        return dto;
        
    }
}