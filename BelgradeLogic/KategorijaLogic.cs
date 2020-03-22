using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public class KategorijaLogic : IKategorijaLogic
    {
        private BeogradContext _beogradContext;

        public KategorijaLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }
        public async Task<bool> Delete(int id)
        {
            Kategorija k = await _beogradContext.Kategorije.FirstOrDefaultAsync(x => x.KategorijaID == id);
            if (k == null)
                return false;
            _beogradContext.Remove(k);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<Kategorija> Find(int id)
        {
            return await _beogradContext.Kategorije.FirstOrDefaultAsync(x => x.KategorijaID == id);
        }

        public async Task<List<Kategorija>> GetObjects()
        {
            return await _beogradContext.Kategorije.ToListAsync();
        }

        public async Task<List<Dogadjaj>> GetObjectsByKategorija(string kategorija)
        {
            List<Dogadjaj> dogadjaji;
            if (string.IsNullOrEmpty(kategorija))
            {
                dogadjaji = _beogradContext.Dogadjaji.ToList();
            }
            else {
                dogadjaji = _beogradContext.Dogadjaji.
                Where(p => p.KategorijeDogadjaji.Any(k => k.Kategorija.Naziv == kategorija)).ToList();
            }
            return dogadjaji;
        }

        public async Task<bool> Insert(Kategorija kategorija)
        {
            try
            {
                _beogradContext.Kategorije.Add(kategorija);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>" + ex.Message);
                return false;
            }
        }


        public async Task<bool> Update(Kategorija kategorija)
        {
            Kategorija k = await _beogradContext.Kategorije.FirstOrDefaultAsync(x => x.KategorijaID == kategorija.KategorijaID);
            if (k == null)
                return false;
            k.Naziv = kategorija.Naziv;
            _beogradContext.Kategorije.Update(k);
            await _beogradContext.SaveChangesAsync();
            return true;
        }
    }
}