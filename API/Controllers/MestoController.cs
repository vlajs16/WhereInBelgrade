using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BelgradeLogic;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
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
        public ActionResult<IEnumerable<Mesto>> Get()
        {
            return _mestoLogic.GetObjects();
        }

        // GET: api/Mesto/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Mesto> Get(int id)
        {
            Mesto m = _mestoLogic.Find(id);
            if (m == null)
                return NotFound($"Vrednost sa id-em: {id} ne postoji!");
            return m;
        }

        // POST: api/Mesto
        [HttpPost]
        public IActionResult Post([FromBody] Mesto mesto)
        {
            if (!_mestoLogic.Insert(mesto))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Mesto/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Mesto mesto)
        {
            mesto.MestoID = id;
            if (!_mestoLogic.Update(mesto))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_mestoLogic.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
