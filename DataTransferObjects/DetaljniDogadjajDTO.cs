﻿using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace DataTransferObjects
{
    public class DetaljniDogadjajDTO
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public virtual Mesto Lokacija { get; set; }
        public KategorijaZaDogadjajDTO MainKategorija { get; set; }
        public virtual List<KategorijaZaDogadjajDTO> Kategorije { get; set; }
        public bool Lajkovan { get; set; } = false;
    }
}
