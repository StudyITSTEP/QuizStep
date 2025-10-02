using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.QuestionAnswer;
using QuizStep.Core.Errors.General;

namespace QuizStep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionAnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetQuestionAnswerByIdQuery(id));
            if (!result) return NotFound(result.Error?.Description);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetQuestionAnswersQuery());
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return BadRequest(result.Error?.Description);
            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateQuestionAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Error == QueryError.EntityNotExist)
                return NotFound();
            if (!result) return BadRequest(result.Error?.Description);
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteQuestionAnswerCommand(id));
            if (result.Error == QueryError.EntityNotExist)
                return NotFound();
            if (!result) return BadRequest(result.Error?.Description);
            return Ok();
        }
    }
}
