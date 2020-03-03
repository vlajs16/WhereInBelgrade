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
        public Mesto Lokacija { get; set; }
        public List<Komentar> Komentari { get; set; }
        public List<KategorijaDogadjaj> kategorijeDogadjaji { get; set; }

    }
}
