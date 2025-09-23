using AutoMapper;
using QuizStep.Application.Commands___Queries.QuizResult;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class QuizResultProfile: Profile
{
    public QuizResultProfile()
    {
        CreateMap<QuizResultDto, QuizResult>().ReverseMap();
        CreateMap<QuizResult, SetQuizResultCommand>().ReverseMap();
    }
}