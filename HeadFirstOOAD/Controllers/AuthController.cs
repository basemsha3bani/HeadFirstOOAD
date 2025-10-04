using Application1.Features.Users.Queries;
using Application1.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IMediator  _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] UsersViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery(model);
            var result = await _mediator.Send(getUserByIdQuery);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
