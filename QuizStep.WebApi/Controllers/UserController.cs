using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizStep.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // [HttpGet("{userId}")]
    // public async Task<IActionResult> GetUserDetails(string userId)
    // {
    //           asd  
    // }
}