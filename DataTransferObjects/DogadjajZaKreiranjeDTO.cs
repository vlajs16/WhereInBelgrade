using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class DogadjajZaKreiranjeDTO
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public IFormFile Image { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        //public virtual Mesto Lokacija { get; set; }
        public int MestoID { get; set; }
        public int? Kategorija1 { get; set; }
        public int? Kategorija2 { get; set; }
        public int? Kategorija3 { get; set; }
        public int? Kategorija4 { get; set; }
        public int? Kategorija5 { get; set; }
        //public virtual List<KategorijaDogadjajInsertDTO> Kategorije { get; set; }
    }
}
