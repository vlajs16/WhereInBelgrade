using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BelgradeLogic;
using DataAccessLayer;
using DataTransferObjects;
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
        private IMapper _mapper;

        public KategorijaController(IKategorijaLogic kategorijaLogic, IMapper mapper)
        {
            _kategorijaLogic = kategorijaLogic;
            _mapper = mapper;
        }
        // GET: api/Kategorija
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kategorijeFromRepo = await _kategorijaLogic.GetObjects();
            var kategorijeZaVracanje = _mapper.Map<List<OnlyKategorijaDTO>>(kategorijeFromRepo);
            return Ok(kategorijeZaVracanje);
        }

        // GET: api/Kategorija/5
        [HttpGet("{id}", Name = "GetKategorije")]
        public async Task<IActionResult> Get(int id)
        {
            Kategorija k = await _kategorijaLogic.Find(id);
            if(k==null)
                return NotFound($"Kategorija sa id-em: {id} ne postoji!");
            return Ok(k);
        }

        [HttpGet("{kategorija}/dogadjaji")]
        public async Task<IActionResult> Get(string kategorija)
        {
            List<Dogadjaj> dogadjajiIzBaze = await _kategorijaLogic.GetObjectsByKategorija(kategorija);
            List<DogadjajiZaKategorijuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajiZaKategorijuDTO>>(dogadjajiIzBaze);
            return Ok(dogadjajiZaVracanje);
        }

        [HttpGet("dogadjaji")]
        public async Task<IActionResult> Get(bool provera)
        {
            List<Dogadjaj> dogadjajiIzBaze = await _kategorijaLogic.GetObjectsByKategorija("");
            List<DogadjajiZaKategorijuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajiZaKategorijuDTO>>(dogadjajiIzBaze);
            return Ok(dogadjajiZaVracanje);
        }

        // POST: api/Kategorija
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Kategorija kategorija)
        {
            if (!await _kategorijaLogic.Insert(kategorija))
                return BadRequest();
            return Ok();
        }

        // PUT: api/Kategorija/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Kategorija kategorija)
        {
            kategorija.KategorijaID = id;
            if (!await _kategorijaLogic.Update(kategorija))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _kategorijaLogic.Delete(id))
                return BadRequest();
            return Ok();
        }
    }
}
