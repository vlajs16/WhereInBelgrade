using DataAccessLayer;
using Helpers;
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
    public class SvidjanjeLogic : ISvidjanjeLogic
    {
        private BeogradContext _beogradContext;
        public SvidjanjeLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }
        public async Task<PagedList<Svidjanje>> GetObjects(EventParams eventParams, int korisnikId)
        {
            var svidjanja = _beogradContext.Svidjanja.Where(s => s.KorisnikID == korisnikId)
                .OrderByDescending(s => s.DogadjajID);
            return await PagedList<Svidjanje>.CreateAsync(svidjanja, eventParams.PageNumber, eventParams.PageSize);

        }

        public async Task<bool> Insert(Svidjanje svidjanje)
        {
            try
            {
                _beogradContext.Svidjanja.Add(svidjanje);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>" + ex.Message);
                return false;
            }
        }
        public async Task<bool> Delete(int korisnikId, int dogadjajId)
        {
            Svidjanje s = await _beogradContext.Svidjanja.FirstOrDefaultAsync(x => 
                            (x.KorisnikID == korisnikId && x.DogadjajID == dogadjajId));
            if (s == null)
                return false;
            _beogradContext.Remove(s);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<Korisnik> GetUser(int userId)
        {
            Korisnik k = await _beogradContext.Korisnici.FirstOrDefaultAsync(p => p.KorisnikID == userId);
            return k;
        }
    }
}
