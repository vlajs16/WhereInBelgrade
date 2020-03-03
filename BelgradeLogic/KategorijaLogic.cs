using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BelgradeLogic
{
    public class KategorijaLogic : IKategorijaLogic
    {
        private BeogradContext _beogradContext;

        public KategorijaLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }
        public bool Delete(int id)
        {
            Kategorija k = _beogradContext.Kategorije.FirstOrDefault(x => x.KategorijaID == id);
            if (k == null)
                return false;
            _beogradContext.Remove(k);
            _beogradContext.SaveChanges();
            return true;
        }

        public Kategorija Find(int id)
        {
            return _beogradContext.Kategorije.FirstOrDefault(x => x.KategorijaID == id);
        }

        public List<Kategorija> GetObjects()
        {
            return _beogradContext.Kategorije.ToList();
        }

        public bool Insert(Kategorija kategorija)
        {
            try
            {
                _beogradContext.Kategorije.Add(kategorija);
                _beogradContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>" + ex.Message);
                return false;
            }
        }

        public bool Update(Kategorija kategorija)
        {
            Kategorija k = _beogradContext.Kategorije.FirstOrDefault(x => x.KategorijaID == kategorija.KategorijaID);
            if (k == null)
                return false;
            k.Naziv = kategorija.Naziv;
            _beogradContext.Kategorije.Update(k);
            _beogradContext.SaveChanges();
            return true;
        }
    }
}