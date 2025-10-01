using AutoMapper;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class AnswerProfile: Profile
{
    public AnswerProfile()
    {
        CreateMap<AnswerDto, Answer>().ReverseMap();
    }
}