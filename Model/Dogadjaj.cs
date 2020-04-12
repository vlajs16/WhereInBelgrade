using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Dogadjaj
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public virtual Mesto Lokacija { get; set; }
        public virtual List<Komentar> Komentari { get; set; }
        public virtual List<KategorijaDogadjaj> KategorijeDogadjaji { get; set; }
        public virtual List<Svidjanje> Svidjanja { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
