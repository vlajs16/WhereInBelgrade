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

        // override object.Equals
        [System.ComponentModel.EditorBrowsable]
        public override bool Equals(object obj)
        {
            if (obj == null ||  
                !(obj.GetType().Name.Contains(this.GetType().Name) || this.GetType().Name.Contains(obj.GetType().Name)))
            {
                return false;
            }


            KategorijaDogadjaj pom = (KategorijaDogadjaj)obj;

            if (pom.DogadjajID != this.DogadjajID)
                return false;
            if (pom.KategorijaID != this.KategorijaID)
                return false;
            return true;
        }

    }
}
