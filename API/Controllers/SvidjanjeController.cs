using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BelgradeLogic;
using DataAccessLayer;
using DataTransferObjects;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/likes")]
    [ApiController]
    public class SvidjanjeController : ControllerBase
    {
        private ISvidjanjeLogic _svidjanjeLogic;
        private readonly IMapper _mapper;

        public SvidjanjeController(ISvidjanjeLogic svidjanjeLogic, IMapper mapper)
        {
            _svidjanjeLogic = svidjanjeLogic;
            _mapper = mapper;
        }
        // GET: api/Svidjanje
        [HttpGet]
        public async Task<IActionResult> Get(int userId, [FromQuery] EventParams eventParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _svidjanjeLogic.GetUser(userId);
            var likes = await _svidjanjeLogic.GetObjects(eventParams, userId);
            var likedEvents = _mapper.Map<List<DogadjajSvidjanjeDTO>>(likes);

            Response.AddPagination(likes.CurrentPage, likes.PageSize,
                likes.TotalCount, likes.TotalPages);

            return Ok(likedEvents);
        }

        // POST: api/Svidjanje
        [HttpPost("{id:int}")]
        public async Task<IActionResult> Post(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _svidjanjeLogic.GetUser(userId);

            if (userFromRepo.Svidjanja.Any(p => p.DogadjajID == id))
                return BadRequest("You have already liked this event");

            if (!await _svidjanjeLogic.Insert(new Svidjanje 
            { 
                KorisnikID = userId,
                DogadjajID = id
            }))
                return BadRequest();
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _svidjanjeLogic.GetUser(userId);

            if (!userFromRepo.Svidjanja.Any(p => p.DogadjajID == id))
                return BadRequest("This user did not liked selected event");

            if (!await _svidjanjeLogic.Delete(userId, id))
                return BadRequest();
            return Ok();
        }
    }
}
