using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Svidjanje
    {
        public int DogadjajID { get; set; }
        public virtual Dogadjaj Dogadjaj { get; set; }
        public int KorisnikID { get; set; }
    }
}
