using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelgradeLogic;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/mesto")]
    [ApiController]
    public class MestoController : ControllerBase
    {
        private IMestoLogic _mestoLogic;
        private BeogradContext _beogradContext;

        public MestoController(IMestoLogic mestoLogic, BeogradContext beogradContext)
        {
            _mestoLogic = mestoLogic;
            _beogradContext = beogradContext;
        }
        // GET: api/Mesto
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return  Ok(await _mestoLogic.GetObjects());
        }

        // GET: api/Mesto/5
        //[AllowAnonymous]
        [HttpGet("{id}", Name = "GetMesta")]
        public async Task<IActionResult> Get(int id)
        {
            Mesto m = await  _mestoLogic.Find(id);
            if (m == null)
                return NotFound($"Vrednost sa id-em: {id} ne postoji!");
            return Ok(m);
        }

        // POST: api/Mesto
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Mesto mesto)
        {
            if (!await _mestoLogic.Insert(mesto))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Mesto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Mesto mesto)
        {
            mesto.MestoID = id;
            if (!await _mestoLogic.Update(mesto))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _mestoLogic.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
