using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.Quiz;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController: ControllerBase
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
}