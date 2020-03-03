using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects
{
    public class KorisnikZaRegistracijuDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage ="You must enter password with 4 to 20 chars")]
        public string Password { get; set; }
    }
}
