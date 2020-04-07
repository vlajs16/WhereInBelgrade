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
    [Route("api/contactemail")]
    [ApiController]
    public class ContactEmailController : ControllerBase
    {
        private IContactEmailLogic emailLogic;
        public ContactEmailController(IContactEmailLogic emailLogic)
        {
            this.emailLogic = emailLogic;
        }
        
        // POST: api/ContactEmail
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactEmail email)
        {
            if (!await emailLogic.SendEmail(email))
                return BadRequest();
            return Ok();
        }
    }
}
