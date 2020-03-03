using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelgradeLogic;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Controllers
{
    [Route("api/svidjanje")]
    [ApiController]
    public class SvidjanjeController : ControllerBase
    {
        private ISvidjanjeLogic _svidjanjeLogic;
        public SvidjanjeController(ISvidjanjeLogic svidjanjeLogic)
        {
            _svidjanjeLogic = svidjanjeLogic;
        }
        // GET: api/Svidjanje
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _svidjanjeLogic.GetObjects());
        }

        // POST: api/Svidjanje
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Svidjanje value)
        {
            if (!await _svidjanjeLogic.Insert(value))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{korisnickiId:int}/{dogadjajId:int}")]
        public async Task<IActionResult> Delete(int korisnickiId, int dogadjajId)
        {

            if (!await _svidjanjeLogic.Delete(korisnickiId, dogadjajId))
                return BadRequest();
            return Ok();
        }
    }
}
