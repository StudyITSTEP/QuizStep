using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.Answer;
using QuizStep.Core.Errors.General;
using System.Threading;

namespace QuizStep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetAnswerByIdQuery(id));
            if (!result) return NotFound(result.Error);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAnswersQuery());
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Error?.Code == QueryError.EntityNotExist.Code)
                return NotFound(result.Error);

            if (!result) return BadRequest(result.Error);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAnswerCommand(id));
            if (result.Error?.Code == QueryError.EntityNotExist.Code)
                return NotFound(result.Error);

            if (!result) return BadRequest(result.Error);
            return Ok(result);
        }
    }
}
