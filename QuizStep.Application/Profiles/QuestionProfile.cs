using AutoMapper;
using QuizStep.Application.Commands___Queries.Question;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class QuestionProfile: Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, CreateQuestionCommand>().ReverseMap();
        CreateMap<Question, UpdateQuestionCommand>().ReverseMap();
        CreateMap<QuestionDto, Question>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
            .ReverseMap();
    }
}