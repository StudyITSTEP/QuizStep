using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Errors.General;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.Quiz;

public class GetQuizDetailsQueryHandler : IRequestHandler<GetQuizDetailsQuery, Result<QuizDetailsDto>>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly IQuizProvider _quizProvider;
    private readonly IQuizResultProvider _quizResultProvider;

    public GetQuizDetailsQueryHandler(IUser user, IQuizProvider quizProvider, IMapper mapper,
        IQuizResultProvider quizResultProvider)
    {
        _user = user;
        _quizProvider = quizProvider;
        _mapper = mapper;
        _quizResultProvider = quizResultProvider;
    }

    public async Task<Result<QuizDetailsDto>> Handle(GetQuizDetailsQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizProvider.GetByIdAsync(request.QuizId, cancellationToken);
        if (quiz == null) return QueryError.EntityNotExist;

        var dto = new QuizDetailsDto();

        var creator = await _user.GetUserByIdAsync(quiz.CreatorId!);
        if (creator == null) return QueryError.EntityNotExist;
        var average = await _quizResultProvider.GetAverageScoreByQuizAsync(quiz.Id);
        var totalParticipants = await _quizResultProvider.GetTotalParticipantsAsync(quiz.Id);
        var fullName = creator.FirstName + " " + creator.LastName;

        return new QuizDetailsDto()
        {
            Id = quiz.Id,
            CreatorId = creator.Id,
            CreatorEmail = creator.Email,
            TotalQuestions = quiz.Questions.Count,
            TotalParticipants = totalParticipants.Value,
            AverageScore = average.Value,
            Access = quiz.Access ?? 0,
            Description = quiz.Description,
            Name = quiz.Name,
            CreatorName = fullName,
        };
    }
}