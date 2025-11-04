using Application;
using Application1.Features.Guitars.Commands;
using Application1.Features.Guitars.Commands.Handlers;
using Application1.Features.Guitars.Queries;
using Application1.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors]
    [ApiController]
    public class GuitarsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IDistributedCache _cache;

        public GuitarsController(IMediator mediator,IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        // GET: api/Guitars
        [HttpGet("{id}")]
       
        public async Task<ActionResult<IEnumerable<GuitarViewModel>>> GetGuitarById(string id)
        {
            var cachedguitar = await _cache.GetStringAsync(id);
                if(cachedguitar != null)
            {
                return Ok(JsonConvert.DeserializeObject<GuitarViewModel>(cachedguitar));

            }
            if (!await GuitarViewModelExists(id))
            {
                return NotFound();
            }
            await _cache.SetStringAsync(_guitarViewModel.serialNumber, JsonConvert.SerializeObject(_guitarViewModel));
            DateTime currentdate = DateTime.Now;
            string time = currentdate.ToLocalTime().ToString();
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(3)
            };
            _cache.Set("Time", Encoding.UTF8.GetBytes(time), cacheOptions);
            return Ok(_guitarViewModel);
        }
        [Route("GetGuitars")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<GuitarViewModel>>> GetGuitars([FromBody] GuitarViewModel SearchCriteria=null)
        {
            var query = new ListGuitarsQuery(SearchCriteria);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

       

        // PUT: api/Guitars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuitarViewModel(string id, GuitarViewModel GuitarViewModel)
        {
            if (id != GuitarViewModel.serialNumber)
            {
                return BadRequest();
            }

            

            try
            {
                UpdateGuitarCommand updateGuitarCommand = (UpdateGuitarCommand)GuitarViewModel;
               
                await _mediator.Send(updateGuitarCommand);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GuitarViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Guitars {"serialNumber":"1","price":"1","builder":"builder1","model":"model1","type":"type1","backWood":"backWood1","topWood":"topWood1"}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
       [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        [Route("PostGuitar")]
        public async Task<ActionResult> PostGuitarViewModel(GuitarViewModel GuitarViewModel)
        {
           
            try
            {
                AddGuitarCommand addGuitarCommand = (AddGuitarCommand)GuitarViewModel;

                await _mediator.Send(addGuitarCommand);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/Guitars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GuitarViewModel>> DeleteGuitarViewModel(string id)
        {
           
            DeleteGuitarCommand deleteGuitarCommand = new DeleteGuitarCommand { serialNumber=id};
            var orders = await _mediator.Send(deleteGuitarCommand);
            
            return Ok();

        }
        private GuitarViewModel _guitarViewModel;
        private async Task<bool> GuitarViewModelExists(string id)
        {
            var query = new GetGuitarByIdQuery(new GuitarViewModel { serialNumber=id});
            var guitar = await _mediator.Send(query);
            if(guitar!=null)
            {
                _guitarViewModel = guitar;
            }
            return guitar != null;
        }
    }
}
