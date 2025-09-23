using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.Category;
using QuizStep.Core.Errors.General;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (!result) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Error == QueryError.EntityNotExist) return NotFound();
        else if (!result) return BadRequest();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        if (result.Error == QueryError.EntityNotExist) return NotFound();
        else if (!result) return BadRequest();
        return Ok(result);
    }
}