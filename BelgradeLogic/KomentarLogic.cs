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
    public class KomentarLogic : IKomentarLogic
    {
        private BeogradContext _beogradContext;
        public KomentarLogic(BeogradContext beogradContext)
        {
            _beogradContext = beogradContext;
        }


        public async Task<bool> Delete(int korisnikId, int dogadjajId)
        {
            Komentar k = await _beogradContext.Komentari.FirstOrDefaultAsync(x =>
                            (x.KorisnikID == korisnikId && x.DogadjajID == dogadjajId));
            if (k == null)
                return false;
            _beogradContext.Remove(k);
            await _beogradContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Komentar>> GetObjects()
        {
            return await _beogradContext.Komentari.ToListAsync();
        }

        public async Task<bool> Insert(Komentar komentar)
        {
            try
            {
                _beogradContext.Komentari.Add(komentar);
                await _beogradContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(">>>>>>>>>>>" + ex.Message);
                return false;
            }
        }
    }
}
