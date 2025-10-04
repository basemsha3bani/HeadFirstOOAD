
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ServicesClasses.Interfaces;
using System.Threading.Tasks;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

      

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] UsersViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        //[HttpPost("addrole")]
        //public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _authService.AddRoleAsync(model);

        //    if (!string.IsNullOrEmpty(result))
        //        return BadRequest(result);

        //    return Ok(model);
        //}
    }
}
