using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> Delete(int id)
        {
            Mesto m = await _beogradContext.Mesta.FirstOrDefaultAsync(p=> p.MestoID == id);
            if (m == null)
                return false;
            _beogradContext.Remove(m);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<Mesto> Find(int id)
        {
            return await _beogradContext.Mesta.FirstOrDefaultAsync(p => p.MestoID == id);
        }

        public async Task<List<Mesto>> GetObjects()
        {
            return await _beogradContext.Mesta.ToListAsync();
        }

        public async Task<bool> Insert(Mesto mesto)
        {
            try
            {
                _beogradContext.Mesta.Add(mesto);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Mesto mesto)
        {
            Mesto m = await _beogradContext.Mesta.FirstOrDefaultAsync(p => p == mesto);
            if (m == null)
                return false;
            m.Naziv = mesto.Naziv;
            m.Sprat = mesto.Sprat;
            m.Ulica = mesto.Ulica;
            m.BrojStana = mesto.BrojStana;
            m.BrojUlice = mesto.BrojUlice;
            _beogradContext.Mesta.Update(m);
            await _beogradContext.SaveChangesAsync();
            return true;
        }
    }
}
