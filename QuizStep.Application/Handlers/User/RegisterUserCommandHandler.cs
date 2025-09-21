using System.Web;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Errors.UserErrors;
using QuizStep.Core.Events.UserEvents;
using QuizStep.Core.Interfaces;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Handlers.User;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResultDto>
{
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(IUser user, IMapper mapper, IEmailSender emailSender,
        ILogger<RegisterUserCommandHandler> logger)
    {
        _user = user;
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<RegisterResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Core.Entities.User>(request);
        var result = await _user.CreateUserAsync(user, request.Password!);
        var dto = new RegisterResultDto();
        _logger.LogDebug($"Created user result: {result}");

        if (result.Succeeded)
        {
            dto.Result = Result.Success();
            var confirmToken = await _user.GenerateEmailConfirmationTokenAsync(user);
            var uriBuilder = new UriBuilder(request.ConfirmationLink);
            uriBuilder.Query = confirmToken;

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = confirmToken;
            query["email"] = request.Email;
            uriBuilder.Query = query.ToString();
            var link = uriBuilder.Uri.ToString();

            _emailSender.SendEmailAsync(user.Email, "Confirm Email", link);
            _logger.LogDebug($"Email sent");
        }
        else dto.Result = RegisterError.RegisterFailed;

        return dto;
    }
}