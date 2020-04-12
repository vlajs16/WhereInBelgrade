using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class DogadjajiZaKategorijuDTO
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumPocetka { get; set; }
        public Mesto Lokacija { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
