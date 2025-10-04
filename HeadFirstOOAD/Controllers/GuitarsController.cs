using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ServicesClasses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

using Application;


using MediatR;
using Application1.Features.Guitars.Queries;
using Application1.ViewModels;
using Application1.Features.Guitars.Commands.Handlers;
using Application1.Features.Guitars.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
   //[EnableCors]
    [ApiController]
    public class GuitarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuitarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Guitars
        [HttpPost]
        [Route("GetGuitars")]
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
        [Authorize(Roles = "admin")]
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

        private async Task<bool> GuitarViewModelExists(string id)
        {
            var query = new GetGuitarByIdQuery(new GuitarViewModel { serialNumber=id});
            var orders = await _mediator.Send(query);
            return orders != null;
        }
    }
}
