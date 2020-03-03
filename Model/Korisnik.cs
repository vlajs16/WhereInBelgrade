using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Korisnik
    {

        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public virtual List<Svidjanje> Svidjanja { get; set; }
    }
}
