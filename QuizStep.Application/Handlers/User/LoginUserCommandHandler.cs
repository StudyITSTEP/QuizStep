using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.User;

public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, LoginDto>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IUser user,  IMapper mapper)
    {
        _user = user;
        _mapper = mapper;
    }
    
    public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Core.Entities.User>(request);
        var result = await _user.CheckPasswordAsync(user, request.Password!);
        return _mapper.Map<LoginDto>(result);
    }
}