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
    [Route("api/kategorija")]
    [ApiController]
    public class KategorijaController : ControllerBase
    {
        private IKategorijaLogic _kategorijaLogic;
        private BeogradContext _beogradContext;

        public KategorijaController(IKategorijaLogic kategorijaLogic, BeogradContext beogradContext)
        {
            _kategorijaLogic = kategorijaLogic;
            _beogradContext = beogradContext;
        }
        // GET: api/Kategorija
        [HttpGet]
        public ActionResult<IEnumerable<Kategorija>> Get()
        {
            return _kategorijaLogic.GetObjects();
        }

        // GET: api/Kategorija/5
        [HttpGet("{id}", Name = "GetKategorije")]
        public ActionResult<Kategorija> Get(int id)
        {
            Kategorija k = _kategorijaLogic.Find(id);
            if(k==null)
                return NotFound($"Kategorija sa id-em: {id} ne postoji!");
            return k;
        }

        // POST: api/Kategorija
        [HttpPost]
        public IActionResult Post([FromBody] Kategorija kategorija)
        {
            if (!_kategorijaLogic.Insert(kategorija))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Kategorija/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Kategorija kategorija)
        {
            kategorija.KategorijaID = id;
            if (!_kategorijaLogic.Update(kategorija))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_kategorijaLogic.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
