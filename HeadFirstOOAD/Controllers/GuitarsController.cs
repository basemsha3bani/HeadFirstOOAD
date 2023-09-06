using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using ServicesClasses;
using Microsoft.AspNetCore.Cors;

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
        public async Task<ActionResult<IEnumerable<GuitarDataModel>>> GetGuitars([FromBody] GuitarDataModel SearchCriteria=null)
        {
            return await _GuitarService.list(SearchCriteria);
        }

        // GET: api/Guitars/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GuitarDataModel>> GetGuitarDataModel(string id)
        //{
        //    var GuitarDataModel = await _GuitarService.GetById(int.Parse(id));

        //    if (GuitarDataModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return GuitarDataModel;
        //}

        // PUT: api/Guitars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuitarDataModel(string id, GuitarDataModel GuitarDataModel)
        {
            if (id != GuitarDataModel.serialNumber)
            {
                return BadRequest();
            }

            

            try
            {
               
                await _GuitarService.Edit(GuitarDataModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuitarDataModelExists(id))
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
        [HttpPost]
        [Route("PostGuitar")]
        public async Task<ActionResult> PostGuitarDataModel(GuitarDataModel GuitarDataModel)
        {
           
            try
            {
                await _GuitarService.Add(GuitarDataModel);
            }
            catch (DbUpdateException)
            {
                if (GuitarDataModelExists(GuitarDataModel.serialNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGuitarDataModel", new { id = GuitarDataModel.serialNumber }, GuitarDataModel);
        }

        // DELETE: api/Guitars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GuitarDataModel>> DeleteGuitarDataModel(string id)
        {
            await _GuitarService.Delete(int.Parse(id));
            return Ok();

        }

        private bool GuitarDataModelExists(string id)
        {
            return _GuitarService.GetById(int.Parse(id)) != null ;
        }
    }
}
