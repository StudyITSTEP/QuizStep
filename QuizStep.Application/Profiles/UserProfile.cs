using AutoMapper;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Entities;

namespace QuizStep.Application.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, LoginUserCommand>();
        CreateMap<LoginDto, User>();
    }
}