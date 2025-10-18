using Application1.Features.Users.Queries;
using Application1.ViewModels;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Threading.Tasks;
using Utils.Events;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IMediator  _mediator;
        IPublishEndpoint _publishEndPoint;
        public AuthController(IMediator mediator, IPublishEndpoint publishEndPoint)
        {
            _mediator = mediator;
            _publishEndPoint = publishEndPoint;
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
            //rabbit mq
          await  _publishEndPoint.Publish(new UserLoginEvent { UserName=model.UserName}); ;
            return Ok(result);
        }
    }
}
