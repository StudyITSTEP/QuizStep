using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.User;
using QuizStep.Application.DTOs.User;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var command = _mapper.Map<RegisterUserCommand>(dto);
        command.ConfirmationLink = Url.Action(nameof(ConfirmEmail), "Account", null, Request.Scheme);

        var result = await _mediator.Send(command);
        if (!result.Result!) return Conflict(result);
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Result!) return Unauthorized(result);
        return Ok(result);
    }

    [HttpGet]
    [Route("email-confirm")]
    public async Task<IActionResult> ConfirmEmail(string? token, string? email)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand(token, email));
        return Ok(result);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([Bind("RefreshToken, UserId")] RefreshTokenDto dto)
    {
        var command = _mapper.Map<RefreshTokenCommand>(dto);
        var result = await _mediator.Send(command);
        if (result == null) return Unauthorized();
        return Ok(result);
    }
}