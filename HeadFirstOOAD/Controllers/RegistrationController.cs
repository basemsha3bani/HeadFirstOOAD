using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Commands;
 
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new user by sending a CreateUserCommand via MediatR.
        /// </summary>
        /// <param name="command">CreateUserCommand containing user registration data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>ActionResult with command handler result.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                return BadRequest("Request body must contain a CreateUserCommand.");
            }

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
