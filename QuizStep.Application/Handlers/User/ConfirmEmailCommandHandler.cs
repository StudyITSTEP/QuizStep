using MediatR;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.User;

public class ConfirmEmailCommandHandler: IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly IUser _user;

    public ConfirmEmailCommandHandler(IUser user)
    {
        _user = user;
    }
    
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _user.GetUserByEmailAsync(request.Email);
        if (user == null) return null;
        return await _user.ConfirmEmailAsync(user, request.Token);
    }
}