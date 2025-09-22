using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.AccessHandlers.Requirements;
using QuizStep.Application.Commands___Queries.Quiz;
using QuizStep.WebApi.Filters.AuthorizationFilters;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetQuizzesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [TypeFilter<QuizAccessAuthorizationFilter>]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetByIdQuizQuery() { Id = id });
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateQuizCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteQuizCommand() { Id = id });
        if (!result) return NotFound();
        return Ok(result);
    }
}