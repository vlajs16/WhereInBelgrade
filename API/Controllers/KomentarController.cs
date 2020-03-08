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
    [Route("api/komentar")]
    [ApiController]
    public class KomentarController : ControllerBase
    {
        private IKomentarLogic _komentarLogic;

        public KomentarController(IKomentarLogic komentarLogic)
        {
            _komentarLogic = komentarLogic;
        }


        // GET: api/Komentar
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _komentarLogic.GetObjects());

        }

       

        // POST: api/Komentar
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Komentar komentar)
        {

            if (!await _komentarLogic.Insert(komentar))
                return BadRequest();
            return Ok();
        }

     

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{korisnickiId}/{dogadjajId}")]
        public async Task<IActionResult> Delete(int korisnickiId, int dogadjajId)
        {
            if (!await _komentarLogic.Delete(korisnickiId,dogadjajId))
                return BadRequest();
            return Ok();


        }
    }
}
