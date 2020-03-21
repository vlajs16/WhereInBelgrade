using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class KategorijaDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<DogadjajiZaKategorijuDTO> Dogadjaji { get; set; }
    }
}
