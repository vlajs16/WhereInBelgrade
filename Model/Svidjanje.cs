using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Svidjanje
    {
        public int DogadjajID { get; set; }
        public Dogadjaj Dogadjaj { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
