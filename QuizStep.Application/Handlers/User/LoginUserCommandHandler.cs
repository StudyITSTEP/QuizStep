using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Errors.UserErrors;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginResultDto>>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IUser user, IMapper mapper)
    {
        _user = user;
        _mapper = mapper;
    }

    public async Task<Result<LoginResultDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _user.GetUserByEmailAsync(request.Email);
        var result = await _user.CheckPasswordAsync(user, request.Password!);
        LoginResultDto dto = new();
        if (result)
        {
            var token = await _user.GetAccessTokenAsync(user);
            var refreshToken = await _user.GenerateRefreshTokenAsync(user.Id);
            dto.Token = token;
            dto.RefreshToken = refreshToken!;
        }
        else
        {
            return LoginError.UserOrPassword;
        }

        return dto;
    }
}