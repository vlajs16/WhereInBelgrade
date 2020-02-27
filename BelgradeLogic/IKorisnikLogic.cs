using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BelgradeLogic
{
    public interface IKorisnikLogic
    {
        List<Korisnik> GetObjects();
        bool Insert(Korisnik korisnik);
        bool Update(Korisnik korisnik);
        bool Delete(int id);
        Mesto Find(int id);




    }
}
