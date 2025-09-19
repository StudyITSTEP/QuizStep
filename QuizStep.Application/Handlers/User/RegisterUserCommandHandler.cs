using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.User;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterDto>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUser user, IMapper mapper)
    {
        _user = user;
        _mapper = mapper;
    }

    public async Task<RegisterDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Core.Entities.User>(request);
        var result = await _user.CreateUserAsync(user, request.Password!);
        var dto = _mapper.Map<RegisterDto>(user);

        if (result.Succeeded) dto.Success = true;

        return dto;
    }
}