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
    public class DogadjajLogic : IDogadjajLogic
    {
        private BeogradContext _beogradContext;
        public DogadjajLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }
        public async Task<bool> Delete(int id)
        {
            Dogadjaj d = await _beogradContext.Dogadjaji.FirstOrDefaultAsync(x => x.DogadjajID == id);
            if (d == null)
                return false;
            _beogradContext.Dogadjaji.Remove(d);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<Dogadjaj> Find(int id)
        {
            return await _beogradContext.Dogadjaji.FirstOrDefaultAsync(x => x.DogadjajID == id);
        }

        public async Task<List<Dogadjaj>> GetObjects()
        {
            return await _beogradContext.Dogadjaji.ToListAsync();
        }

        public async Task<bool> Insert(Dogadjaj dogadjaj)
        {
            try
            {
                _beogradContext.Dogadjaji.Add(dogadjaj);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(Dogadjaj dogadjaj)
        {
            Dogadjaj d = await _beogradContext.Dogadjaji.FirstOrDefaultAsync(p => p == dogadjaj);
            if (d == null)
                return false;
            d.Naziv = dogadjaj.Naziv;
            d.Opis = dogadjaj.Opis;
            d.Lokacija = dogadjaj.Lokacija;
            d.KategorijeDogadjaji = dogadjaj.KategorijeDogadjaji;
            d.DatumPocetka = dogadjaj.DatumPocetka;
            d.DatumZavrsetka = dogadjaj.DatumZavrsetka;
            d.Komentari = dogadjaj.Komentari;
            _beogradContext.Dogadjaji.Update(d);
            await _beogradContext.SaveChangesAsync();
            return true;
        }
    }
}
