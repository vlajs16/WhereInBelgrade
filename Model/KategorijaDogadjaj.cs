using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class KategorijaDogadjaj
    {
        public int KategorijaID { get; set; }
        public int DogadjajID { get; set; }
        public Kategorija Kategorija { get; set; }
        public Dogadjaj Dogadjaj { get; set; }
    }
}
