using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class KategorijaDogadjaj
    {
        public int KategorijaID { get; set; }
        public int DogadjajID { get; set; }
        public virtual Kategorija Kategorija { get; set; }
        public virtual Dogadjaj Dogadjaj { get; set; }
    }
}
