using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BelgradeLogic;
using DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthLogic _logic;
        private readonly IConfiguration _config;

        public AuthController(IAuthLogic logic, IConfiguration config)
        {
            _logic = logic;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(KorisnikZaRegistracijuDTO korisnik)
        {
            korisnik.Username = korisnik.Username.ToLower();

            if (await _logic.UserExists(korisnik.Username))
                return BadRequest("Username already exists!");

            var korisnikZaKreiranje = new Korisnik
            {
                Username = korisnik.Username
            };

            var createdUser = await _logic.Register(korisnikZaKreiranje, korisnik.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(KorisnikZaLogovanjeDTO korisnikZaLogovanjeDto)
        {
            var userFromRepo = await _logic.Login(korisnikZaLogovanjeDto.Username.ToLower(),
                korisnikZaLogovanjeDto.Password.ToLower());
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, userFromRepo.KorisnikID.ToString()),
               new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                admin = userFromRepo.Admin
            });
        }
    }
}
