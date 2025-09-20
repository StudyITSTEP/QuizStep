using AutoMapper;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginUserCommand, User>();
        CreateMap<User, LoginDto>();
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<User, RegisterDto>();
        CreateMap<RegisterUserCommand, RegisterDto>().ReverseMap();
        CreateMap<RefreshTokenCommand, RefreshTokenDto>().ReverseMap();
    }
}