﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class DogadjajZaListuDTO
    {
        public int DogadjajID { get; set; }
        public string Naziv { get; set; }
        public int ZaKolikoDana { get; set; }
        public KategorijaZaDogadjajDTO MainKategorija { get; set; }
    }
}
