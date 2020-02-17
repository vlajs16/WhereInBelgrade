using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataAccessLayer;
using Model;

namespace BelgradeLogic
{
    public class MestoLogic : IMestoLogic
    {
        private BeogradContext _beogradContext;

        public MestoLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }

        public bool Delete(int id)
        {
            Mesto m = _beogradContext.Mesta.FirstOrDefault(p=> p.MestoID == id);
            if (m == null)
                return false;
            _beogradContext.Remove(m);
            _beogradContext.SaveChanges();
            return true;
        }

        public Mesto Find(int id)
        {
            return _beogradContext.Mesta.FirstOrDefault(p => p.MestoID == id);
        }

        public List<Mesto> GetObjects()
        {
            return _beogradContext.Mesta.ToList();
        }

        public bool Insert(Mesto mesto)
        {
            try
            {
                _beogradContext.Mesta.Add(mesto);
                _beogradContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(Mesto mesto)
        {
            Mesto m = _beogradContext.Mesta.FirstOrDefault(p => p == mesto);
            if (m == null)
                return false;
            m.Naziv = mesto.Naziv;
            m.Sprat = mesto.Sprat;
            m.Ulica = mesto.Ulica;
            m.BrojStana = mesto.BrojStana;
            m.BrojUlice = mesto.BrojUlice;
            _beogradContext.Mesta.Update(m);
            _beogradContext.SaveChanges();
            return true;
        }
    }
}
