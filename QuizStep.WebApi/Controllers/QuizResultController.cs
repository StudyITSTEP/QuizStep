using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.QuizResult;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizResultController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizResultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("result")]
    public async Task<IActionResult> GetQuizResult([FromQuery] GetQuizResultQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.Value == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Route("set-result")]
    public async Task<IActionResult> SetQuizResult([FromBody] SetQuizResultCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpGet("results/{userId}")]
    public async Task<IActionResult> GetQuizResultsByUserId(string userId)
    {
        var result = await _mediator.Send(new GetQuizResultsByUserIdQuery(userId));
        if (result.Value == null) return NotFound();
        return Ok(result.Value);
    }
}