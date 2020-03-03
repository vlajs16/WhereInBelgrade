using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<List<Svidjanje>> GetObjects()
        {
            return await _beogradContext.Svidjanja.ToListAsync();
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
    }
}
