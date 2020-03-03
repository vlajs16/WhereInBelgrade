using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelgradeLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/dogadjaj")]
    [ApiController]
    public class DogadjajController : ControllerBase
    {
        private IDogadjajLogic _dogadjajLogic;
        public DogadjajController(IDogadjajLogic dogadjajLogic)
        {
            _dogadjajLogic = dogadjajLogic;
        }
        // GET: api/Dogadjaj
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dogadjajLogic.GetObjects());
        }

        // GET: api/Dogadjaj/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Dogadjaj d = await _dogadjajLogic.Find(id);
            if (d == null)
                return BadRequest();
            return Ok(d);
        }

        // POST: api/Dogadjaj
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dogadjaj value)
        {
            if (!await _dogadjajLogic.Insert(value))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Dogadjaj/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dogadjaj dogadjaj)
        {
            dogadjaj.DogadjajID = id;
            if (!await _dogadjajLogic.Update(dogadjaj))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _dogadjajLogic.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
