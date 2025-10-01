using AutoMapper;
using QuizStep.Application.DTOs.Quiz;
using QuizStep.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.Profiles
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<QuizDto, Quiz>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                .ReverseMap();
        }
    }
}