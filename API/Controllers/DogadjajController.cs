using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BelgradeLogic;
using DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public DogadjajController(IDogadjajLogic dogadjajLogic, IMapper mapper)
        {
            _dogadjajLogic = dogadjajLogic;
            _mapper = mapper;
        }
        // GET: api/Dogadjaj
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Dogadjaj> dogadjajiIzBaze = await _dogadjajLogic.GetObjects();
            List<DogadjajZaListuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajZaListuDTO>>(dogadjajiIzBaze);
            return Ok(await _dogadjajLogic.GetObjects());
        }

        [HttpGet("kategorija/{kategorija}")]
        public async Task<IActionResult> Get(string kategorija)
        {
            List<Dogadjaj> dogadjajiIzBaze = await _dogadjajLogic.GetObjectsByKategorija(kategorija);
            List<DogadjajZaListuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajZaListuDTO>>(dogadjajiIzBaze);
            return Ok(dogadjajiZaVracanje);
        }

        // GET: api/Dogadjaj/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Dogadjaj d = await _dogadjajLogic.Find(id);
            if (d == null)
                return BadRequest();
            DetaljniDogadjajDTO dogadjajZaSlanje = _mapper.Map<DetaljniDogadjajDTO>(d);
            return Ok(dogadjajZaSlanje);
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

        [Authorize]
        [HttpGet("{id}/user/{userId:int}")]
        public async Task<IActionResult> Get(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Dogadjaj d = await _dogadjajLogic.Find(id);
            if (d == null)
                return BadRequest();

            DetaljniDogadjajDTO dogadjajZaSlanje = _mapper.Map<DetaljniDogadjajDTO>(d);

            if (d.Svidjanja.Any(p => p.KorisnikID == userId))
                dogadjajZaSlanje.Lajkovan = true;

            return Ok(dogadjajZaSlanje);
        }
    }
}
