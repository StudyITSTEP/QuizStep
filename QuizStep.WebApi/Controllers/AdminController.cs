using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizStep.Application.Commands___Queries.User;

namespace QuizStep.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            if (result.Succeeded && result.Value != null)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpPost("set-role")]
        public async Task<IActionResult> SetRole([FromBody] SetUserRoleCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Error);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId));

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Error);
        }
    }
}