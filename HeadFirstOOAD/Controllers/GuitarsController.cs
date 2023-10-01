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
using ViewModel;

namespace HeadFirstOOAD.Controllers
{
    [Route("api/[controller]")]
   //[EnableCors]
    [ApiController]
    public class GuitarsController : ControllerBase
    {
        private readonly GuitarService _GuitarService;

        public GuitarsController(GuitarService GuitarService)
        {
            _GuitarService = GuitarService;
        }

        // GET: api/Guitars
        [HttpPost]
        [Route("GetGuitars")]
        public async Task<ActionResult<IEnumerable<GuitarViewModel>>> GetGuitars([FromBody] GuitarViewModel SearchCriteria=null)
        {
            return await _GuitarService.list(SearchCriteria);
        }

        // GET: api/Guitars/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GuitarViewModel>> GetGuitarViewModel(string id)
        //{
        //    var GuitarViewModel = await _GuitarService.GetById(int.Parse(id));

        //    if (GuitarViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return GuitarViewModel;
        //}

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
               
                await _GuitarService.Edit(GuitarViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuitarViewModelExists(id))
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
                await _GuitarService.Add(GuitarViewModel);
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
            await _GuitarService.Delete(int.Parse(id));
            return Ok();

        }

        private bool GuitarViewModelExists(string id)
        {
            return _GuitarService.GetById(int.Parse(id)) != null ;
        }
    }
}
