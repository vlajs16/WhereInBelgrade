using System;

namespace Model
{
    public class Mesto
    {
        public int MestoID { get; set; }
        public string Naziv { get; set; }
        public string Ulica { get; set; }
        public string BrojUlice { get; set; }
        public int Sprat { get; set; }
        public int BrojStana { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Mesto m = (Mesto)obj;
            if (this.MestoID != m.MestoID)
                return false;
            return true;
        }
    }
}
