using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataAccessLayer;
using Model;

namespace BelgradeLogic
{
    public class KorisnikLogic : IKorisnikLogic
    {
        private BeogradContext _beogradContext;

        public KorisnikLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }

        public bool Delete(int id)
        {
            Korisnik k = _beogradContext.Korisnici.FirstOrDefault(p => p.KorisnikID == id);
            if (k == null)
                return false;
            _beogradContext.Remove(k);
            _beogradContext.SaveChanges();
            return true;
        }

        public Korisnik Find(int id)
        {
            return _beogradContext.Korisnici.FirstOrDefault(p => p.KorisnikID == id);
        }

        public List<Korisnik> GetObjects()
        {
            return _beogradContext.Korisnici.ToList();
        }

        public bool Insert(Korisnik korisnik)
        {
            try
            {
                _beogradContext.Korisnici.Add(korisnik);
                _beogradContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(Korisnik korisnik)
        {
            Korisnik k = _beogradContext.Korisnici.FirstOrDefault(p => p == korisnik);
            if (k == null)
                return false;
            k.Admin = korisnik.Admin;
            k.Username = korisnik.Username;
            k.Password = korisnik.Password;
            _beogradContext.Korisnici.Update(k);
            _beogradContext.SaveChanges();
            return true;
        }

        Mesto IKorisnikLogic.Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
