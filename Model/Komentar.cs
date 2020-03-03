using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Komentar
    {
        public DateTime DatumVreme { get; set; }
        public string Tekst { get; set; }
        public virtual Dogadjaj Dogadjaj { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public int DogadjajID { get; set; }
        public int KorisnikID { get; set; }
    }
}
