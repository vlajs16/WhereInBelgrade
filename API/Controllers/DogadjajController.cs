using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BelgradeLogic;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataTransferObjects;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;

namespace API.Controllers
{
    [Route("api/dogadjaj")]
    [ApiController]
    public class DogadjajController : ControllerBase
    {
        private IDogadjajLogic _dogadjajLogic;
        private Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly IKorisnikLogic _korisnikLogic;
        private readonly ISvidjanjeLogic _svidjanjeLogic;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IMestoLogic _mestoLogic;
        private readonly IKategorijaLogic _kategorijaLogic;

        public DogadjajController(IDogadjajLogic dogadjajLogic, IMapper mapper, 
            IKorisnikLogic korisnikLogic, ISvidjanjeLogic svidjanjeLogic,
            IOptions<CloudinarySettings> cloudinaryConfig, IMestoLogic mestoLogic,
            IKategorijaLogic kategorijaLogic)
        {
            _dogadjajLogic = dogadjajLogic;
            _mapper = mapper;
            _korisnikLogic = korisnikLogic;
            _svidjanjeLogic = svidjanjeLogic;
            _cloudinaryConfig = cloudinaryConfig;
            _mestoLogic = mestoLogic;
            _kategorijaLogic = kategorijaLogic;
            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }
        // GET: api/Dogadjaj
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EventParams eventParams)
        {
            var dogadjaji = await _dogadjajLogic.GetObjects(eventParams);
            List<DogadjajZaListuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajZaListuDTO>>(dogadjaji);

            Response.AddPagination(dogadjaji.CurrentPage, dogadjaji.PageSize, 
                dogadjaji.TotalCount, dogadjaji.TotalPages);

            return Ok(dogadjajiZaVracanje);
        }

        [HttpGet("kategorija/{kategorija}")]
        public async Task<IActionResult> Get(string kategorija, [FromQuery] EventParams eventParams)
        {
            var dogadjaji = await _dogadjajLogic.GetObjectsByKategorija(eventParams, kategorija);
            List<DogadjajZaListuDTO> dogadjajiZaVracanje =
                _mapper.Map<List<DogadjajZaListuDTO>>(dogadjaji);
            Response.AddPagination(dogadjaji.CurrentPage, dogadjaji.PageSize,
                dogadjaji.TotalCount, dogadjaji.TotalPages);
            return Ok(dogadjajiZaVracanje);
        }

        [HttpGet("pocetna/{kategorija}")]
        public async Task<IActionResult> GetPocetna(string kategorija)
        {
            List<Dogadjaj> dogadjajiIzBaze = await _dogadjajLogic.GetObjectsByKategorijaThree(kategorija);
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
        [Authorize]
        [HttpPost("user/{userId}")]
        public async Task<IActionResult> Post([FromForm] DogadjajZaKreiranjeDTO dogadjajZaKreiranjeDTO, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Niste prijavljeni");
            if (!await _korisnikLogic.IsAdmin(userId))
                return Unauthorized("Vi niste admin");

            var file = dogadjajZaKreiranjeDTO.Image;

            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            dogadjajZaKreiranjeDTO.Url = uploadResult.Uri.ToString();
            dogadjajZaKreiranjeDTO.PublicId = uploadResult.PublicId;

            var eventForDb = _mapper.Map<Dogadjaj>(dogadjajZaKreiranjeDTO);
            eventForDb.Lokacija = await _mestoLogic.Find(dogadjajZaKreiranjeDTO.MestoID);

            if (!await _dogadjajLogic.Insert(eventForDb))
                return BadRequest("Neuspešno sačuvan dogadjaj");

            List<Kategorija> kategorije = new List<Kategorija>();
            if(dogadjajZaKreiranjeDTO.Kategorija1 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija1 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija2 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija2 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija3 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija3 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija4 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija4 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija5 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija5 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }

            if (kategorije != null)
            {
                eventForDb.KategorijeDogadjaji = Helpers.Extensions.ConvertToEvent(kategorije, eventForDb.DogadjajID);

                if (!await _dogadjajLogic.Update(eventForDb))
                    return BadRequest("Kategorije neuspešno sačuvane");
            }

            return Ok();
        }

        // PUT: api/dogadjaj/user/2
        [Authorize]
        [HttpPut("user/{userId}")]
        public async Task<IActionResult> Put([FromForm] DogadjajZaKreiranjeDTO dogadjajZaKreiranjeDTO, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Niste prijavljeni");
            if (!await _korisnikLogic.IsAdmin(userId))
                return Unauthorized("Vi niste admin");

            //trazim oiginalne podatke o tom dogadjaju
            Dogadjaj d = await _dogadjajLogic.Find(dogadjajZaKreiranjeDTO.DogadjajID);
            if (d == null)
                return BadRequest();
            DogadjajiZaKategorijuDTO originalanDogadjaj = _mapper.Map<DogadjajiZaKategorijuDTO>(d);

            //provera da li je promenjena slika
            var file = dogadjajZaKreiranjeDTO.Image;
            var uploadResult = new ImageUploadResult();
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
                dogadjajZaKreiranjeDTO.Url = uploadResult.Uri.ToString();
                dogadjajZaKreiranjeDTO.PublicId = uploadResult.PublicId;
            }
            else
            {
                dogadjajZaKreiranjeDTO.Url = originalanDogadjaj.Url.ToString();
                dogadjajZaKreiranjeDTO.PublicId = originalanDogadjaj.PublicId;
            }
            

            var eventForDb = _mapper.Map<Dogadjaj>(dogadjajZaKreiranjeDTO);
            eventForDb.Lokacija = await _mestoLogic.Find(dogadjajZaKreiranjeDTO.MestoID);

            //if (!await _dogadjajLogic.Update(eventForDb))
            //    return BadRequest("Neuspešno izmenjen dogadjaj");

            List<Kategorija> kategorije = new List<Kategorija>();
            if (dogadjajZaKreiranjeDTO.Kategorija1 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija1 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija2 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija2 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija3 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija3 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija4 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija4 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }
            if (dogadjajZaKreiranjeDTO.Kategorija5 != null)
            {
                int id = dogadjajZaKreiranjeDTO.Kategorija5 ?? default(int);
                kategorije.Add(await _kategorijaLogic.Find(id));
            }

            if (kategorije != null)
            {
                eventForDb.KategorijeDogadjaji = Helpers.Extensions.ConvertToEvent(kategorije, eventForDb.DogadjajID);
            }
            try
            {
                if (!await _dogadjajLogic.Update2(eventForDb))
                    return BadRequest("Kategorije neuspešno sačuvane");
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>" + ex.Message);
                return BadRequest("Kategorije neuspešno sačuvane");
            }
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
