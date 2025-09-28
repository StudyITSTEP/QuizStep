using AutoMapper;
using MediatR;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Interfaces;

namespace QuizStep.Application.Handlers.User;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto?>
{
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokenCommandHandler(IMapper mapper, IUser user, IJwtProvider jwtProvider)
    {
        _mapper = mapper;
        _user = user;
        _jwtProvider = jwtProvider;
    }

    public async Task<RefreshTokenDto?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _user.GetUserByIdAsync(request.UserId);
        if (user == null)
        {
            return null;
        }

        var newToken = await _user.RenewRefreshTokenAsync(user.Id, request.RefreshToken, TimeSpan.FromDays(7));
        if (newToken == null)
        {
            return null;
        }

        var token = _jwtProvider.GetJwt(user);

        return new RefreshTokenDto
        {
            AccessToken = token,
            RefreshToken = newToken,
        };
    }
}